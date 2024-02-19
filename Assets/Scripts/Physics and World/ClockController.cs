//author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//A script to control the sol cycle


public class ClockController : MonoBehaviour
{
    [Tooltip("Drag every NPC and Flower in here.")]
    [SerializeField]
    private GameObject[] alarmRecipients;

    //The end of the rotation in the circle
    private int maximum = 360;

    //The start of the rotation in the circle
    private int minimum = 0;
    private int currMinimum = 0; //The starting degrees of the current cycle

    [Tooltip("Enter as fraction of 24h (8/24 = 8am, 20/24 = 8pm)")]
    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float earlyDayStart = (8 / 24);
    private int earlyDayStartDegrees; //The calculated degrees of earlyDayStart

    [Tooltip("Enter as fraction of 24h (8/24 = 8am, 20/24 = 8pm)")]
    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float lateDayStart = (12 / 24);
    private int lateDayStartDegrees; //The calculated degrees of lateDayStart

    [Tooltip("Enter as fraction of 24h (8/24 = 8am, 20/24 = 8pm)")]
    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float earlyDayEnd = (18 / 24);
    private int earlyDayEndDegrees; //The calculated degrees of earlyDayEnd

    [Tooltip("Enter as fraction of 24h (8/24 = 8am, 20/24 = 8pm)")]
    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float lateDayEnd = (23 / 24);
    private int lateDayEndDegrees; //The calculated degrees of lateDayEnd

    [Tooltip("Enter as fraction of 24h (8/24 = 8am, 20/24 = 8pm)")]
    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float nightStart = (23 / 24);
    private int nightStartDegrees; //The calculated degrees of nightStart

    [Tooltip("Enter as fraction of 24h (8/24 = 8am, 20/24 = 8pm)")]
    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float nightEnd = (3 / 24);
    private int nightEndDegrees; //The calculated degrees of nightEnd

    //The calculated percentile of each integer
    private float percentile;

    //The time left in the current cycle
    private float timeLeft;

    [Tooltip("How long 24h take in the game (in seconds)")]
    [SerializeField] //The duration of a cycle
    private float dayDuration = 600.0f;

    [HideInInspector]
    public bool isDay = true; //Is it currently day?
    private bool startNight = true; //Is it between any dayStart and earlyDayEnd?
    private bool startLateDay = false; //Does the day or night end without the player ending it manually?

    [HideInInspector]
    public Quaternion rotation;

    [HideInInspector]
    public Vector3 spawnPos;

    [HideInInspector]
    public Quaternion spawnRotation;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private LightChanges lightSource;

    [SerializeField]
    private SleepDialogue sleepUI;

    private bool isFirstDay = true;

    // Start is called before the first frame update
    void Start()
    {
        isFirstDay = true;
        spawnRotation = player.gameObject.transform.rotation;
        spawnPos = player.gameObject.transform.position;
        //Calculate x/24 into degrees of a circle with 0 being minimum and 24 being maximum, in base config 0=0°, 24=360°
        percentile = ((float)maximum - (float)minimum) / dayDuration;
        earlyDayStartDegrees = (int)(minimum - (earlyDayStart * dayDuration * percentile));
        lateDayStartDegrees = (int)(minimum - (lateDayStart * dayDuration * percentile));
        earlyDayEndDegrees = (int)(minimum - (earlyDayEnd * dayDuration * percentile));
        lateDayEndDegrees = (int)(minimum - (lateDayEnd * dayDuration * percentile));
        nightStartDegrees = (int)(minimum - (nightStart * dayDuration * percentile));
        nightEndDegrees = (int)(minimum - (nightEnd * dayDuration * percentile));
        Time.timeScale = 1;
        StartDay();
    }

    // Update is called once per frame
    void Update()
    {
        {
            float currPercentile;
            //The current rotation
            currPercentile = (timeLeft * percentile + currMinimum);
            //Passage of time
            timeLeft -= Time.deltaTime;
            //Rotating the game object
            gameObject.transform.rotation = Quaternion.Euler(0, 0, (float)minimum + currPercentile);

            //Automatic events
            if (isDay) //During the day
            {
                //Is it after earlyDayEnd?
                if (currPercentile <= maximum + earlyDayEndDegrees)
                {
                    //Sleeping won't start the night
                    startNight = false;
                }

                //if lateDayEnd has been reached
                if (currPercentile <= maximum + lateDayEndDegrees)
                {
                    //The player is gonna oversleep
                    startLateDay = true;
                    EndDay();
                }
            }
            else //During the night
            {
                //if nightEnd has been reached
                if (currPercentile <= nightEndDegrees)
                {
                    //The player is gonna oversleep
                    startLateDay = true;
                    EndDay();
                }
            }
        }
    }

    //When the day ends, do stuff
    //Called when interacting with a bed
    public void EndDay()
    {
        if (startNight)
            StartNight();
        else
            StartDay();
    }

    //Start night
    public void StartNight()
    {
        isDay = false;
        startNight = false;
        //Set start to night start
        currMinimum = nightStartDegrees;
        //Reset timer%
        timeLeft = dayDuration;
        player.gameObject.transform.position = spawnPos;
        player.gameObject.transform.rotation = spawnRotation;
        Alarm();
    }

    //start new day
    public void StartDay()
    {
        //if player is gonna oversleep, change day start to late day start
        if (startLateDay)
            currMinimum = lateDayStartDegrees;
        else
            currMinimum = earlyDayStartDegrees;

        player.gameObject.transform.position = spawnPos;
        player.gameObject.transform.rotation = spawnRotation;
        player.ResetAllInteractables();

        //reset bools and timer%
        timeLeft = dayDuration;
        isDay = true;
        startNight = true;
        Alarm();
        startLateDay = false;
    }

    //Called when player wakes up
    //Alternative to broadcast system
    private void Alarm()
    {
        //goes through every recipient
        foreach (GameObject recipient in alarmRecipients)
        {
            //If it's a pickup item
            if (recipient.TryGetComponent<PickupItem>(out PickupItem currItem))
            {
                //Interact with the current object
                currItem.StartTime(isDay);
            }
            //If it's an NPC
            else if (recipient.TryGetComponent<NPC>(out NPC currNPC))
            {
                //Interact with the current NPC
                currNPC.StartTime(isDay);
            }
        }
        //Change colour to day/night colour
        lightSource.ChangeColor(isDay);
        if (!isFirstDay)
            sleepUI.DialogueStart(startLateDay, !isDay);

        isFirstDay = false;
    }
}
