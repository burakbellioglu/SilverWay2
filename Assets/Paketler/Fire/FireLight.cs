using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireLight : MonoBehaviour
{
    public Light2D light2d;
    float lightInt;
    public float minInt = 3f, maxInt = 5f;


    private void Start()
    {
       InvokeRepeating("FireEffect", 0, 0.2f);
    }

  
    private void FireEffect()
    {
        lightInt = Random.Range(minInt, maxInt);
        light2d.pointLightOuterRadius = lightInt;
    }
}
