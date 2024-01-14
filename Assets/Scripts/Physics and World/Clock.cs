//author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//A script to control the sol cycle


public class Clock : MonoBehaviour
{
    private EventManager<GlobalEvent> globalEventManager = new EventManager<GlobalEvent>();

    [SerializeField] //The end of the rotation in the circle
    private int maximum = 360;

    [SerializeField] //The start of the rotation in the circle
    private int minimum = 0;
    private int currMinimum = 0; //The starting degrees of the current cycle

    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float earlyDayStart = (8 / 24);
    private int earlyDayStartDegrees; //The calculated degrees of earlyDayStart

    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float lateDayStart = (12 / 24);
    private int lateDayStartDegrees; //The calculated degrees of lateDayStart

    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float earlyDayEnd = (18 / 24);
    private int earlyDayEndDegrees; //The calculated degrees of earlyDayEnd

    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float lateDayEnd = (23 / 24);
    private int lateDayEndDegrees; //The calculated degrees of lateDayEnd

    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float nightStart = (23 / 24);
    private int nightStartDegrees; //The calculated degrees of nightStart

    [SerializeField] //Enter in inspector as fraction of 24h (8/24 = 8am, 20/24 = 8pm)
    private float nightEnd = (3 / 24);
    private int nightEndDegrees; //The calculated degrees of nightEnd

    //The calculated percentile of each integer
    private float percentile;

    //The time left in the current cycle
    public float timeLeft;

    [SerializeField] //The duration of a cycle
    private float dayDuration = 600.0f;
    public bool isDay = true; //Is it currently day?
    private bool startNight = true; //Is it between any dayStart and earlyDayEnd?
    private bool startLateDay = false; //Does the day or night end without the player ending it manually?
    public Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        percentile = ((float)maximum - (float)minimum) / dayDuration;
        earlyDayStartDegrees = (int)(minimum - (earlyDayStart * dayDuration * percentile));
        lateDayStartDegrees = (int)(minimum - (lateDayStart * dayDuration * percentile));
        earlyDayEndDegrees = (int)(minimum - (earlyDayEnd * dayDuration * percentile));
        lateDayEndDegrees = (int)(minimum - (lateDayEnd * dayDuration * percentile));
        nightStartDegrees = (int)(minimum - (nightStart * dayDuration * percentile));
        nightEndDegrees = (int)(minimum - (nightEnd * dayDuration * percentile));
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
        globalEventManager.Broadcast<bool>(GlobalEvent.StartTime, isDay);
    }

    //start new day
    public void StartDay()
    {
        //FIXME: Reload scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        //if player is gonna oversleep, change day start to late day start
        if (startLateDay)
            currMinimum = lateDayStartDegrees;
        else
            currMinimum = earlyDayStartDegrees;

        //reset bools and timer%
        //FIXME: RESET JOURNAL
        timeLeft = dayDuration;
        isDay = true;
        startLateDay = false;
        startNight = true;
       // globalEventManager.Broadcast<bool>(GlobalEvent.StartTime, isDay);
    }
}
