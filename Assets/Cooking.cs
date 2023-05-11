using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    public GameObject white;
    public GameObject yolk;

    private bool cooking;
    private bool burning;

    private float albedo;
    private float yolk_color;

    private float startTime;
    private float timeRn;

    // Start is called before the first frame update
    void Start()
    {
        cooking = false;
        burning = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(cooking){
            CookEgg();
        } else if(burning){
            BurnEgg();
        } else {
            GetButtonPress();
        }
    }

    void GetButtonPress()
    {
        //if the button is pressed, continue drawing rays at teleportation locations
        bool isPressed = OVRInput.Get(OVRInput.RawButton.X);
        if(isPressed){
            cooking = true;
            startTime = Time.time;
        }
    }

    void CookEgg()
    {
        timeRn = Time.time - startTime;
        albedo = (timeRn*5 + 60)/255;

        if(albedo < 0.75){
            yolk_color = (timeRn*2 + 190)/255;
            if(yolk_color < 1){
                yolk.GetComponent<Renderer>().material.color = new Color(1, yolk_color, 0, albedo);
            }
            white.GetComponent<Renderer>().material.color = new Color(1, 1, 1, albedo);
        } else {
            cooking = false;
            burning = true;
            startTime = Time.time;
        }
    }

    void BurnEgg()
    {
        timeRn = Time.time - startTime;
        yolk_color = (255 - timeRn*6)/255;

        if(yolk_color > 0){
            yolk.GetComponent<Renderer>().material.color = new Color(yolk_color, yolk_color, 0, 0.75f);
            white.GetComponent<Renderer>().material.color = new Color(yolk_color, yolk_color, yolk_color, 0.75f);
        }
    }
}
