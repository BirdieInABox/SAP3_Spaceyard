//Author: Kim Bolnder
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] //The followed object
    private Transform player;

    [SerializeField] //The camera's x-offset
    private float cameraHeight = 20.0f;

    [SerializeField] //The camera's z-offset
    private float cameraDistance = 10f;
    private Transform cam;

    public void Start()
    {
        cam = transform;
    }

    public void Update()
    {
        //Follow the player
        Vector3 pos = player.position;
        //add offsets
        pos.y += cameraHeight;
        pos.z -= cameraDistance;
        cam.position = pos;
    }
}
