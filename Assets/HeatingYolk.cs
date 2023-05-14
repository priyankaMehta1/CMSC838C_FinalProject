using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingYolk : MonoBehaviour
{
    private float startTime;
    private float timeRn;

    private float albedo;
    private float yolk_color;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeRn = Time.time - startTime;
        albedo = (timeRn*3 + 60)/255;

        if(albedo < 0.75){
            yolk_color = (timeRn*2 + 190)/255;
            if(yolk_color < 1){
                this.GetComponent<Renderer>().material.color = new Color(1, yolk_color, 0, albedo);
            }
        }
    }
}
