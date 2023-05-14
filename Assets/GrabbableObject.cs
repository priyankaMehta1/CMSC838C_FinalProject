using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private bool isGrabbed;
    // Start is called before the first frame update
    void Start()
    {
    	isGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetParent(GameObject parent) {
        transform.parent = parent.transform;
    }

    public void DetachFromParent() {
        transform.parent = null;
    }

    public void Grab(float TriggerPress) {
    	isGrabbed = TriggerPress > 0.0; 
    }
}
