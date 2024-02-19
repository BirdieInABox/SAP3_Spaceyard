//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f; //The walking speed
    private Rigidbody rb;
    private Vector2 rotation = Vector2.zero;
    private Vector2 direction; //The direction the player is facing
    private BoxCollider touchSensor; //A hitbox in front of the player
    private GameObject interactObject; //An interactable object within the touch sensor
    private PickupItem pickupObject; //A pickupable object within the touch sensor
    private ChestController chest; //A chest within the touch sensor
    private NPC interactNPC; //An NPC within the touch sensor
    private MainQuestGate gate; //The interactable gate

    [SerializeField]
    private GameObject journalUI;
    public Animator anims; //The player's animation controller

    public InventorySystem inventory; //The player's inventory
    private bool inDialogue = false; //Is the player currently in a dialogue?
    public bool stopMovement = false;

    //When player is created
    void Start()
    {
        //Get components and set up RB
        rb = GetComponent<Rigidbody>();
        touchSensor = GetComponent<BoxCollider>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        rotation.y = transform.eulerAngles.y;
        anims = this.GetComponent<Animator>();

        //lock and hide curs
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Only allow movement outside dialogues
        if (!inDialogue && !stopMovement)
        {
            //Move
            rb.velocity = new Vector3(
                direction.x * moveSpeed,
                rb.velocity.y,
                direction.y * moveSpeed
            );
            //Rotate
            gameObject.transform.LookAt(
                new Vector3(direction.x, 0, direction.y) + gameObject.transform.position
            );
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        //Walking animation
        anims.SetBool("Walking", rb.velocity != Vector3.zero);
    }

    //Unity Input System, value carrying a vector2 with the movement direction
    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
        Debug.Log(direction);
    }

    //Unity Input system
    public void OnInteract(InputValue value)
    {
        if (!stopMovement)
        { //If there is an interactable object in the touch sensor
            if (interactObject != null)
            {
                //if the interactable object has the InteractableObject component
                if (
                    interactObject.TryGetComponent<InteractableObject>(
                        out InteractableObject currObject
                    )
                )
                {
                    //Interact with the current object
                    currObject.Interaction(this);
                }
                //if the interactable object has the Bed component
                else if (interactObject.TryGetComponent<BedController>(out BedController currBed))
                {
                    //Interact with the bed
                    currBed.Interaction();
                }
            }
            //If there is an NPC in the touch sensor
            else if (interactNPC != null)
            {
                //If the player isn't in a dialogue yet
                if (!inDialogue)
                {
                    //Start a dialogue with the NPC
                    interactNPC.InteractionStart();
                }
                else //If he player is in a dialogue
                {
                    //Coninue the dialogue
                    interactNPC.InteractionContinue();
                }
            }
            //If there is a pickup object in the touch sensor
            else if (pickupObject != null)
            {
                //Add the picked-up item to the inventory
                inventory.AddItem(pickupObject.data, 1);
                //Trigger the item's pick-up method
                pickupObject.OnPickup();
            }
            else if (chest != null)
            {
                chest.Interaction(this);
            }
            else if (gate != null)
            {
                if (!inDialogue)
                {
                    gate.InteractionStart();
                }
                else
                {
                    gate.InteractionContinue();
                }
            }
        }
    }

    public void OnJournal(InputValue value)
    {
        if ((!stopMovement && !journalUI.activeSelf) || (stopMovement && journalUI.activeSelf))
        {
            journalUI.SetActive(!journalUI.activeSelf);
            stopMovement = !stopMovement;
            inventory.hotbar.gameObject.GetComponent<PlayerInput>().enabled =
                !inventory.hotbar.gameObject.GetComponent<PlayerInput>().enabled;
            ToggleCursor();
        }
    }

    private void ToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = !Cursor.visible;
    }

    public void AddItem(ItemData item)
    {
        inventory.AddItem(item, 1);
    }

    //Touch sensor
    void OnTriggerEnter(Collider other)
    {
        //Check of what type the object in the touch sensor is
        if (other.gameObject.tag == "Interactable")
            interactObject = other.gameObject;
        else if (other.gameObject.tag == "NPC")
            interactNPC = other.gameObject.GetComponent<NPC>();
        else if (other.gameObject.tag == "Pickup")
            pickupObject = other.gameObject.GetComponent<PickupItem>();
        else if (other.gameObject.tag == "Chest")
            chest = other.gameObject.GetComponent<ChestController>();
        else if (other.gameObject.tag == "Gate")
            gate = other.gameObject.GetComponent<MainQuestGate>();
    }

    //Touch sensor
    void OnTriggerExit(Collider other)
    { //Check of what type the object leaving the touch sensor is
        if (other.gameObject.tag == "Interactable")
            interactObject = null;
        else if (other.gameObject.tag == "NPC")
            interactNPC = null;
        else if (other.gameObject.tag == "Pickup")
            pickupObject = null;
        else if (other.gameObject.tag == "Chest")
            chest = null;
        else if (other.gameObject.tag == "Gate")
            gate = null;
    }

    //Allow dialogue to change inDIalogue
    public void toggleDialogue()
    {
        inDialogue = !inDialogue;
    }

    //If an object disappears out of the touch sensor, reset the variable
    public void ResetInteractable()
    {
        interactObject = null;
    }

    public void ResetPickup()
    {
        pickupObject = null;
    }

    public void ResetNPC()
    {
        if (interactNPC != null)
            interactNPC.EndDialogue();
        interactNPC = null;
        inDialogue = false;
    }

    public void ResetGate()
    {
        if (gate != null)
            gate.EndDialogue();
        gate = null;
        inDialogue = false;
    }

    private void ResetChest()
    {
        chest = null;
    }

    public void ResetAllInteractables()
    {
        ResetPickup();
        ResetInteractable();
        ResetNPC();
        ResetGate();
        ResetChest();
    }
}
