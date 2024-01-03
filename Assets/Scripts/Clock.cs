//author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//A script to control the sol cycle
public class Clock : MonoBehaviour
{
    [SerializeField] //The end of the rotation in the circle
    private int maximum = 360;

    [SerializeField] //The start of the rotation in the circle
    private int minimum = 0;

    
    //The calculated percentile of each integer
    private float percentile;

    //The time left in the current cycle
    public float timeLeft;

    [SerializeField] //The duration of a cycle
    private float dayDuration = 600.0f;

    // Start is called before the first frame update
    void Start()
    {
        percentile = ((float)maximum - (float)minimum) / dayDuration;
    }

    // Update is called once per frame
    void Update()
    {
        float currPercentile;
        //Passage of time
        timeLeft -= Time.deltaTime;
        //The current rotation
        currPercentile = (timeLeft * percentile);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, (float)minimum + currPercentile);

        //if timer runs out
        if (timeLeft <= 0.0f)
        {
            EndDay();
        }
    }

    //When the day ends, do stuff
    public void EndDay()
    {
        StartDay();
    }

    //After end of day stuff, start new day
    public void StartDay()
    {
        timeLeft = dayDuration;
    }
}
