using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hotbar : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    void Update() { }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        Debug.Log("Help");
    }
}
