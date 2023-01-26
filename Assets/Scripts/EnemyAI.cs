using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Hareket Degerleri")]
    public float speed;
    private bool isFacingRight = false;

    [Header("Raycast")]
    public Transform rayStart;
    public float minDistance;

    [Header("Saldiri Degerleri")]
    public GameObject weaponCollider;
    private Transform target; 
    private bool isAttacking = false;


    [Header("Particle")]
    public GameObject cutParticle;
    public GameObject thrustParticle;


    //Kullanacagi silah belirli olacak daha sonradan degisme yok.
    //particle da belirli sbt olacak silaha gore.


    void Start()
    {
        
    }

    
    void Update()
    {

        //Raycast (Tespit etme)

        RaycastHit2D hit = Physics2D.Linecast(rayStart.position, new Vector2(rayStart.position.x - 10, rayStart.position.y));       

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            
            target = hit.collider.gameObject.transform;

            //Takip
            if (Vector2.Distance(gameObject.transform.position, target.position) > minDistance)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                //Saldýr
                if (!isAttacking)
                {
                    int rndm = Random.Range(0, 2);

                    switch (rndm)
                    {
                        case 0:
                            Attack();
                            break;
                        case 1:
                            Attack_2();
                            break;
                    }
                    
                }
                    
            }
            
        }
        else
        {
            return;
        }        

    }

    private void Attack()
    {

        //Animasyon
       

        //Karakterin yüzü ne tarafta? ona göre particle yönleri degisecek
        int deger;
        if (isFacingRight)
            deger = 1;
        else
            deger = -1;


        //Particle           
        var myObject = Instantiate(cutParticle, weaponCollider.transform.parent.position, cutParticle.transform.rotation);
        myObject.transform.parent = weaponCollider.transform.parent;

        //Oluþmuþ partical objelerinin scale degerlerini duzelt (Bazý objelerin scale ile oynandýgý icin ayný goruntu olusmyur).
        myObject.transform.localScale = new Vector3(deger, 1, 1);

        weaponCollider.GetComponent<BoxCollider2D>().enabled = true; //Silah hasar verebilir.

        StartCoroutine(AttackWait()); //Ayný saldýrý turu icin bekleme suresi.

        Debug.Log("attack");
    }

    private void Attack_2()
    {

        //Animasyon - 2


        //Karakterin yüzü ne tarafta? ona göre particle yönleri degisecek
        int deger;
        if (isFacingRight)
            deger = 1;
        else
            deger = -1;


        //Particle           
        var myObject = Instantiate(thrustParticle, weaponCollider.transform.parent.position, cutParticle.transform.rotation);
        myObject.transform.parent = weaponCollider.transform.parent;

        //Oluþmuþ partical objelerinin scale degerlerini duzelt (Bazý objelerin scale ile oynandýgý icin ayný goruntu olusmyur).
        myObject.transform.localScale = new Vector3(deger, 1, 1);

        weaponCollider.GetComponent<BoxCollider2D>().enabled = true; //Silah hasar verebilir.

        StartCoroutine(AttackWait()); //Ayný saldýrý turu icin bekleme suresi.

        Debug.Log("attack - 2");
    }

    private IEnumerator AttackWait()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }


   
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(rayStart.position, new Vector2(rayStart.position.x - 10, rayStart.position.y));
    }


}
