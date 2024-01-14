using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float gravityStrength;
    Rigidbody rb;
    public Transform planet;
    public float RotationSpeed = 20;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 toCenter = planet.position - transform.position;
        toCenter.Normalize();

        rb.AddForce(toCenter * gravityStrength, ForceMode.Acceleration);

        //Player alignment
        Quaternion q = Quaternion.FromToRotation(transform.up, -toCenter);
        q = q * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
    }
}
