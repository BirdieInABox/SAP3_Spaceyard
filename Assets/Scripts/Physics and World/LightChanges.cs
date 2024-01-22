using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanges : MonoBehaviour
{
    [SerializeField]
    private Color dayColor;

    [SerializeField]
    private Color nightColor;

    [SerializeField]
    private int dayIntensity;

    [SerializeField]
    private int nightIntensity;

    public void ChangeColor(bool isDay)
    {
        Light light = this.gameObject.GetComponent<Light>();
        light.color = isDay ? dayColor : nightColor;
        light.intensity = isDay ? dayIntensity : nightIntensity;
    }
}
