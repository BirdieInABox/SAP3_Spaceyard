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
    private PickupItem pickupObject;

    public Animator anims;

    [SerializeField]
    private GameObject backpack;
    public Inventory inventory;

    //[SerializeField]
    private NPC interactNPC;
    private bool inDialogue = false;

    public void toggleDialogue()
    {
        inDialogue = !inDialogue;
    }

    public void ResetInteractable()
    {
        interactObject = null;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        touchSensor = GetComponent<BoxCollider>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        rotation.y = transform.eulerAngles.y;

        anims = this.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.y * moveSpeed);
        anims.SetBool("Walking", rb.velocity != new Vector3(0, 0, 0));
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void OnInteract(InputValue value)
    {
        if (interactObject != null)
        {
            if (
                interactObject.TryGetComponent<InteractableObject>(
                    out InteractableObject currObject
                )
            )
            {
                currObject.Interaction(this);
            }
            else if (interactObject.TryGetComponent<Bed>(out Bed currBed))
            {
                currBed.Interaction(this);
            }
        }
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
        else if (pickupObject != null)
        {
            inventory.AddItem(pickupObject.data, 1);
            pickupObject.OnPickup();
        }
        /*else if (interactObject != null)
            interactObject.GetComponent<InteractableObject>().Interaction(this);*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
            interactObject = other.gameObject;
        else if (other.gameObject.tag == "NPC")
            interactNPC = other.gameObject.GetComponent<NPC>();
        else if (other.gameObject.tag == "Pickup")
            pickupObject = other.gameObject.GetComponent<PickupItem>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
            interactObject = null;
        else if (other.gameObject.tag == "NPC")
            interactNPC = null;
        else if (other.gameObject.tag == "Pickup")
            pickupObject = null;
    }

    public void OnInventory(InputValue value)
    {
        backpack.SetActive(!backpack.activeSelf);
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = !Cursor.visible;
    }
}
