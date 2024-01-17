//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Just use extra type of Interactable Object? Would need clock reference in IntObj though
public class Bed : MonoBehaviour
{
    [SerializeField]
    private Clock clock;

    //Manually end the day when the player interacts with this
    public void Interaction()
    {
        clock.EndDay();
    }
}
