using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{
    public Rigidbody2D rb;
    public float amount;

    void Start()
    {
        //Baslangicta saga sola dagil
        int rndm = Random.Range(0, 2);
        if (rndm == 0)
            rb.AddForce(Vector2.right * Random.Range(150,350));
        else
            rb.AddForce(Vector2.left * Random.Range(150, 350));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerStats.xp += amount;

            Destroy(gameObject);
        }
    }


}
