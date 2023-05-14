using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControllerScript : MonoBehaviour
{
    private GameObject grabbedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        //To grab GrabbableObjects in a radius
        GetTriggerPress();
    }

    Vector3 GetPointingDir()
    {
        Vector3 worldDir = transform.forward;

        return worldDir;
    }

    Vector3 GetPosition()
    {
        Vector3 worldPos = transform.position;
 
        return worldPos;
    }

    void GetTriggerPress()
    {
        float pressedAmt = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
        if(pressedAmt > 0.0){
            GrabObject(pressedAmt);
        } else if (grabbedObject != null) {
            grabbedObject.GetComponent<GrabbableObject>().Grab(pressedAmt);
            grabbedObject.GetComponent<GrabbableObject>().DetachFromParent();
            grabbedObject = null;
        }
    }

    void GrabObject(float pressedValue)
    {
        if(grabbedObject == null)
        {
            Vector3 origin = GetPosition();
            float radius = 3.0f; // arbitrarily adjust this radius, maybe scale?

            Collider[] hitColliders = Physics.OverlapSphere(origin, radius);
            hitColliders = hitColliders.OrderBy(x => Vector3.Distance(origin,x.transform.position)).ToArray();

            foreach(var hitCollider in hitColliders)
            {
                if(hitCollider.gameObject.CompareTag("Grab")) {
                    grabbedObject = hitCollider.gameObject;
                    grabbedObject.GetComponent<GrabbableObject>().Grab(pressedValue);
                    grabbedObject.GetComponent<GrabbableObject>().SetParent(transform.gameObject);
                    break;
                }
            }
        }

    }
    
}