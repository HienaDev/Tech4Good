using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private GameObject holding;
    public GameObject heldObject;
    private GameObject currentInteractible;

    private HashSet<GameObject> interactibleColliding;


    // Start is called before the first frame update
    private void Start()
    {
        interactibleColliding = new HashSet<GameObject>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractible != null)
            {
                currentInteractible.GetComponent<Interactible>().Interact();
            }
        }
        
        if(heldObject != null)
        {
            Debug.Log("Currently holding " + heldObject.name);
        }
    }

    private void FixedUpdate()
    {
        currentInteractible = GetClosestInteractible();

        if (currentInteractible != null)
        {
            currentInteractible.GetComponent<Interactible>().ActivateOutline();
        }
    }

    private GameObject GetClosestInteractible()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        if(interactibleColliding.Count > 0 )
            foreach (GameObject potentialTarget in interactibleColliding)
            {
                potentialTarget.GetComponent<Interactible>().DeactivateOutline();

                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;

                float dSqrToTarget = directionToTarget.sqrMagnitude;

                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

        return bestTarget;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("I HIT " + collision.gameObject.name);
        Interactible currentInteractible = collision.GetComponent<Interactible>();

        if(currentInteractible != null)
        {
            interactibleColliding.Add(currentInteractible.gameObject);
        }
    }
        
    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactible currentInteractible = collision.GetComponent<Interactible>();

        if (currentInteractible != null)
        {
            interactibleColliding.Remove(currentInteractible.gameObject);
            currentInteractible.DeactivateOutline();
            
        }
    }
}
