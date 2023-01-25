using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Hasar alýndý : " + amount);
    }

}
