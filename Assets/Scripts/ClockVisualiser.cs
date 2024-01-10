using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockVisualiser : MonoBehaviour
{
    [SerializeField]
    private Clock clock;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = clock.rotation;
    }
}
