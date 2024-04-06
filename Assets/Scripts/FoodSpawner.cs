using UnityEngine;

public class FoodSpawner : Interactible
{
    [SerializeField] private GameObject foodPrefab;
    private PlayerInteraction playerInteraction;

    private void Start()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }
    
    public override void Interact()
    {
        if(playerInteraction.heldObject == null)
        {
            GameObject food = Instantiate(foodPrefab);
            playerInteraction.heldObject = food;
        }
        else
        {
            Debug.Log("Already holding " + playerInteraction.heldObject.name);
        }
    }
}
