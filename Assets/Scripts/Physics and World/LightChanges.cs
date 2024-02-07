//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanges : MonoBehaviour
{
    //The colour of daylight
    [SerializeField]
    private Color dayColor;

    //The colour of nightlight
    [SerializeField]
    private Color nightColor;

    //The intensity of daylight
    [SerializeField]
    private int dayIntensity;

    //The intensity of nightlight
    [SerializeField]
    private int nightIntensity;

    //Called on day/night change
    public void ChangeColor(bool isDay)
    {
        Light light = this.gameObject.GetComponent<Light>();
        //Change the light colour depending on day/night
        light.color = isDay ? dayColor : nightColor;
        //Change the light intensity depending on day/night
        light.intensity = isDay ? dayIntensity : nightIntensity;
    }
}
