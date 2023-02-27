using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [Header("Atanacak degerler")]
    public float damage;
    public string targetTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag(targetTag))
        {
            collision.GetComponent<Health>().TakeDamage(damage,transform.root.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;    
    }

}
