using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Interactible
{
    public override void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }
}
