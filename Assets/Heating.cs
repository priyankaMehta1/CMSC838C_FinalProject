using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heating : MonoBehaviour
{
    private float startTime;
    private float timeRn;

    private float albedo;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeRn = Time.time - startTime;
        albedo = (timeRn*5 + 60)/255;

        if(albedo < 0.75){
            this.GetComponent<Renderer>().material.color = new Color(1, 1, 1, albedo);
        }
    }
}
