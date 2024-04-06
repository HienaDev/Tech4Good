using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    [SerializeField] private GameObject outline;
    private bool outlineActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        outline.SetActive(outlineActive);
    }

    public void ActivateOutline() => outlineActive = true;

    public void DeactivateOutline() => outlineActive = false;
}
