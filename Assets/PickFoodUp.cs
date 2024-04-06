using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickFoodUp : MonoBehaviour
{

    [SerializeField] private GameObject holding;
    private GameObject currentObject;
    private GameObject holdingObject;

    private HashSet<GameObject> foodColliding;

    // Start is called before the first frame update
    void Start()
    {
        foodColliding = new HashSet<GameObject>();
        holdingObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObject != null)
            {
                if (holdingObject == null)
                {
                    holdingObject = currentObject;
                    holdingObject.transform.position = holding.transform.position;
                }
                else
                {
                    holdingObject.transform.position = new Vector2(holdingObject.transform.position.x, holding.transform.position.y - 16);
                    holdingObject = null;
                    
                }
            }

        }
    }

    private void FixedUpdate()
    {
        currentObject = GetClosestFood();

        if (currentObject != null)
        {
            currentObject.GetComponent<Food>().ActivateOutline();
        }
       
        if( holdingObject != null) 
        {
            holdingObject.transform.position = holding.transform.position;
        }
        

        
    }

    private GameObject GetClosestFood()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        if(foodColliding.Count > 0 )
            foreach (GameObject potentialTarget in foodColliding)
            {
                potentialTarget.GetComponent<Food>().DeactivateOutline();

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

        //Debug.Log(collision.gameObject);

        Food currentFood = collision.GetComponent<Food>();

        if(currentFood != null)
        {
            foodColliding.Add(currentFood.gameObject);
        }

        if (currentObject != null && holdingObject == null && Input.GetKeyDown(KeyCode.E)) 
        {
            holdingObject = currentObject;
        }
    }
        
    private void OnTriggerExit2D(Collider2D collision)
    {
        Food currentFood = collision.GetComponent<Food>();

        if (currentFood != null)
        {
            foodColliding.Remove(currentFood.gameObject);
            currentFood.DeactivateOutline();
            
        }
    }
}
