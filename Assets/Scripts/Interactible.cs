using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    [SerializeField] protected GameObject outline;

    public abstract void Interact();
    
    public void ActivateOutline()
    {
        outline.SetActive(true);
    }
    
    public void DeactivateOutline()
    {
        outline.SetActive(false);
    }
}
