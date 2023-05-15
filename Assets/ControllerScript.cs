using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControllerScript : MonoBehaviour
{
    private GameObject grabbedObject;

    public ParticleSystem eggWhite;
    public ParticleSystem eggYolk;

    private float startTime;
    private float timeRn;

    private float albedo;
    private float yolk_color;

    private bool cooking;

    // Start is called before the first frame update
    void Start()
    {
        cooking = false;
    }

    void FixedUpdate()
    {
        //To grab GrabbableObjects in a radius
        GetTriggerPress();
        if(!cooking){
            GetXPress();
        } else {
            UpdateEgg();
        }
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

    void UpdateEgg()
    {
        timeRn = Time.time - startTime;
        albedo = (timeRn*5 + 20)/255;

        if(albedo < 0.75){
            yolk_color = (timeRn*3 + 190)/255;
            if(yolk_color < 1){
                eggYolk.GetComponent<Renderer>().material.color = new Color(1, yolk_color, 0, albedo);
            }
            eggWhite.GetComponent<Renderer>().material.color = new Color(1, 1, 1, albedo-0.2f);
        }
    }

    void GetXPress()
    {
        bool isPressed = OVRInput.GetDown(OVRInput.RawButton.X);
        if(isPressed){
            StartEgg();
        }
    }

    void StartEgg()
    {
        startTime = Time.time;
        eggWhite.Play();
        eggYolk.Play();
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