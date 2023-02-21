using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public Animator anim;

    public GameObject cannonball; 

    public Transform firePosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) //ates et
        {
            anim.SetTrigger("fire");
        }
    }

    public void FireBall()
    {
        Instantiate(cannonball, firePosition.position, cannonball.transform.rotation);
    }

}
