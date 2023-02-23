using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public Rigidbody2D rb;

    void Start()
    {
        //Baslangicta saga sola dagil
        int rndm = Random.Range(0, 2);
        if (rndm == 0)
            rb.AddForce(Vector2.right *   Random.Range(400, 700));
        else
            rb.AddForce(Vector2.left * Random.Range(400, 700));

    }
}
