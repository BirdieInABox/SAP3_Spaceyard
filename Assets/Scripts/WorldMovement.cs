using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Rather than moving the character, we will move the world under the character's feet.
public class WorldMovement : MonoBehaviour
{
    private Quaternion rotationDirection;

    [SerializeField]
    private Quaternion rotationSpeed;

    void Update()
    {
        rotateWorld();
    }

    private void rotateWorld()
    {
        gameObject.transform.rotation = new Quaternion(
            gameObject.transform.rotation.x + rotationDirection.x * rotationSpeed.x,
            gameObject.transform.rotation.y + rotationDirection.y * rotationSpeed.y,
            gameObject.transform.rotation.z + rotationDirection.z * rotationSpeed.z,
            0
        );
    }

    public void OnMove(InputValue value)
    {
        rotationDirection = Quaternion.Euler(value.Get<Vector2>());
        Debug.Log(rotationDirection);
    }
}
