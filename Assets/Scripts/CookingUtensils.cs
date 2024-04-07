using System.Collections.Generic;
using UnityEngine;

public class CookingUtensils : Interactible
{
    [SerializeField] private GameObject[] keys;
    [SerializeField] private GameObject[] values;
    private Dictionary<GameObject, GameObject> foodDict;
    private PlayerInteraction playerInteraction;
    [SerializeField] private float timer;
    private float initialTimer;

    private GameObject initialObject;
    private bool cooking = false;
    private bool giveItem = false;

    private void Start()
    {
        foodDict = new Dictionary<GameObject, GameObject>();

        for(int i = 0; i < keys.Length; i++)
        {
            foodDict.Add(keys[i], values[i]);
        }

        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    public override void Interact()
    {
        if(playerInteraction.heldObject != null && !cooking)
        {
            if(foodDict.ContainsKey(playerInteraction.heldObject))
            {
                initialObject = playerInteraction.heldObject;
                Destroy(playerInteraction.heldObject);
                initialTimer = Time.time;
                cooking = true;
            }
        }
        if(playerInteraction.heldObject != null && giveItem)
        {
            GetNewObject(playerInteraction.heldObject);
        }
    }

    private void Update()
    {
        if(cooking)
        {
            if(Time.time - initialTimer > timer)
            {
                cooking = false;
                giveItem = true;
            }
        }
    }

    private void GetNewObject(GameObject originalObject)
    {
        Destroy(playerInteraction.heldObject);
        playerInteraction.heldObject = foodDict[originalObject];
        giveItem = false;
    }
}
