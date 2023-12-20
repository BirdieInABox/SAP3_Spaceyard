using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector2 rotation = Vector2.zero;
    private float maxVelocityChange = 10f;
    private Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        rotation.y = transform.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.y * moveSpeed);
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
        Debug.Log(direction);
    }

    public void OnInteract(InputValue value)
    {
        //Interact
    }
}
