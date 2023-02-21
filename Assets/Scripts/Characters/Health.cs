using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    //Knockbak olayý
    //Silahtan hasar verdigi colliderin health scriptine erisip o objeye knockback baslatmasi.

    [Header("Componentler")]
    public Animator anim;
    private Rigidbody2D rb;

    [Header("Objeler")]
    public float health;
    public GameObject bloodParticle;

    [Header("Xp Atamalarý")]
    public GameObject xp;
    public GameObject bigXip;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float amount, Vector2 _transform)
    {
        StartCoroutine(TakeDamageCoroutine(amount, _transform));
    }

    public IEnumerator TakeDamageCoroutine(float amount, Vector2 _transform)
    {
        anim.SetTrigger("hit");

        Instantiate(bloodParticle, transform.position, transform.rotation);

        if (gameObject.CompareTag("Enemy"))
        {
            if (_transform.x < transform.position.x)
                rb.AddForce(Vector2.right * 7500);
            else
                rb.AddForce(Vector2.left * 7500);
        }
        else
        {
            //float timer = 0;
            //Vector2 position = transform.position;
            //_transform += (Vector2)transform.position;

            //while (timer < 0.1f)
            //{
            //    transform.position = Vector2.Lerp(position, _transform, timer / 0.1f);
            //    timer += Time.deltaTime;
            //    yield return null;
            //}
            //transform.position = new Vector3(_transform.x, _transform.y, 0);
            yield return null;
        }


        health -= amount;

        if(health <= 0)
        {
            anim.SetTrigger("die");
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < Random.Range(0, 5); i++) //Kucuk xp spawn.
        {
            Instantiate(xp, transform.position, xp.transform.rotation);
        }

        for (int i = 0; i < Random.Range(0, 2); i++) //Buyuk xp spawn.
        {
            Instantiate(bigXip, transform.position, xp.transform.rotation);
        }


        Destroy(gameObject);
    }

}
