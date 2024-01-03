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
    private BoxCollider touchSensor;
    private GameObject interactObject;
    public Animator anims;

    //[SerializeField]
    private NPC interactNPC;
    private bool inDialogue = false;

    public void toggleDialogue()
    {
        if (inDialogue)
            inDialogue = false;
        else
            inDialogue = true;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        touchSensor = GetComponent<BoxCollider>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        rotation.y = transform.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        anims = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.y * moveSpeed);
        anims.SetBool("Walk", rb.velocity != new Vector3(0, 0, 0));
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
        Debug.Log(direction);
    }

    public void OnInteract(InputValue value)
    {
        if (interactObject != null)
            interactObject.GetComponent<InteractableObject>().Interaction(this);
        else if (interactNPC != null)
        {
            if (!inDialogue)
            {
                interactNPC.InteractionStart();
            }
            else
            {
                interactNPC.InteractionContinue();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
            interactObject = other.gameObject;
        else if (other.gameObject.tag == "NPC")
            interactNPC = other.gameObject.GetComponent<NPC>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
            interactObject = null;
        else if (other.gameObject.tag == "NPC")
            interactNPC = null;
    }
}
