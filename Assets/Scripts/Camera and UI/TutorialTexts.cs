//Author: Kim Bolender
//From source: https://discussions.unity.com/t/gui-text-always-facing-camera/30803/5
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTexts : MonoBehaviour
{
    void Update()
    {
        Vector3 v = Camera.main.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(Camera.main.transform.position - v);
        transform.rotation = (Camera.main.transform.rotation);
    }
}
