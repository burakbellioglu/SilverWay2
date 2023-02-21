using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public Rigidbody2D rb;

    public int power;

    public GameObject explosion;


    void Start()
    {
        rb.AddForce(Vector2.up * 650);
        rb.AddForce(Vector2.right * power);      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);

            Destroy(gameObject);
        }
    }


}
