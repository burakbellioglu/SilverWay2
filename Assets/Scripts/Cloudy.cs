using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloudy : MonoBehaviour
{

    void Update()
    {
        this.transform.Translate(0.4f * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTrigger");
        if (collision.name == "CloudReSpawnPoint")
        {
            transform.position = new Vector3(-50, transform.position.y,transform.position.z);
        }
       
        
    }

}
