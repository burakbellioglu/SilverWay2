using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Hareket Degerleri")]
    public float speed;
    private bool isFacingRight = false;
    private bool coroutineAllowed_running = true;
    private bool isRunning = true; //Kosuyor
    private bool isFollowing = false; //Takip ediyor
    private bool isWaiting = false; // Bekliyor
    private bool isCatching = true; //Karakteri 10 saniyede bir calisan tespit etme. Sadece fonksiyonda kontrol olmasi icin ilk deger true.

    public Transform groundCheck;

    [Header("Raycast")]
    public Transform rayStart;
    public Transform rayStart_back;
    public Transform rayStart_checkWay;
    public float minDistance;

    [Header("Raycast Mesafeleri")]
    public int rayDistanceFront;
    public int rayDistanceBack;
    public int rayDistanceCheckWay;


    [Header("Saldiri Degerleri")]
    public GameObject weaponCollider;
    private Transform target;
    private bool isAttacking = false;


    [Header("Particle")]
    public GameObject cutParticle;
    public GameObject thrustParticle;
    public GameObject runParticle;
    public GameObject catchLogo;


    //Tespit ettiginde yani normal hareketinden ciktiginda unlem isaretini cikar
    //Her seferinde cikmasin ama



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();

        //Hareket dongusunu baslat
        StartCoroutine(MovementFlip());
    }


    void Update()
    {
        
        //Raycast (Tespit etme)

        RaycastHit2D hit_up = Physics2D.Linecast(rayStart.position, new Vector2(rayStart.position.x + rayDistanceFront, rayStart.position.y));
        RaycastHit2D hit_down = Physics2D.Linecast(new Vector2(rayStart.position.x,rayStart.position.y - 0.2f) , new Vector2(rayStart.position.x + rayDistanceFront, rayStart.position.y-2));
        RaycastHit2D hit_back = Physics2D.Linecast(rayStart_back.position, new Vector2(rayStart_back.position.x + rayDistanceBack, rayStart_back.position.y));
        RaycastHit2D checkWay = Physics2D.Linecast(rayStart_checkWay.position, new Vector2(rayStart_checkWay.position.x + rayDistanceCheckWay, rayStart_checkWay.position.y));


        #region Karar verme
        //Raycast Front tespit ederse
        if (hit_up.collider != null && (hit_up.collider.gameObject.CompareTag("Player")) || hit_down.collider != null && (hit_down.collider.gameObject.CompareTag("Player")))
        {
            //Tespit edildi unlem isaretini spawnla.
            StartCoroutine(SpawnCatchLogo());

            isFollowing = true;
            isWaiting = false;

            if(hit_up.collider != null && (hit_up.collider.gameObject.CompareTag("Player")))
                target = hit_up.collider.gameObject.transform;
            else  
                target = hit_down.collider.gameObject.transform;


            //Takip
            if (Vector2.Distance(gameObject.transform.position, target.position) > minDistance)
            {
                //Animasyon
                anim.SetBool("isRunning", true);
                isRunning = true;

                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, speed * 1.75f * Time.deltaTime);

                //Yolum temiz mi? Engel var mý?
                if (checkWay.collider != null && (checkWay.collider.gameObject.CompareTag("Engel")))
                {
                    //Engel var ama dusman yakinda takip etmem gerek
                    //Engeli gecmek ici zipla

                    rb.velocity = new Vector2(rb.velocity.x, 10);


                }

            }
            else
            {
                //Saldýr

                //Animasyon
                anim.SetBool("isRunning", false);
                isRunning = false;

                if (!isAttacking)
                {
                    isAttacking = true; //Ataga basladi.

                    int rndm = Random.Range(0, 2);

                    switch (rndm)
                    {
                        case 0:
                            StartCoroutine(Attack_1());
                            break;
                        case 1:
                            StartCoroutine(Attack_2());
                            break;
                    }

                }

                //Eger karakter dibinde ise saldiri icin uygun pozisyona gec.
                if (Vector2.Distance(gameObject.transform.position, target.position) < minDistance - 0.3f)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, -speed * Time.deltaTime);
                }
            }

        }

        //Raycast Back tespit ederse
        else if (hit_back.collider != null && (hit_back.collider.gameObject.CompareTag("Player")))
        {
            Flip();
        }

        //Hic dusman yoksa
        else
        {
            isFollowing = false;

            //Patrol ALGORÝTMA
            //Saga veya sola hareket et.
            //Karsina engel cikarsa veya belirli bir sure sonra, bekle.
            //Yönünü degistir ve hareketine devam et.

            //Yolum temiz mi? Engel var mý?
            if (checkWay.collider != null && (checkWay.collider.gameObject.CompareTag("Engel")) && isRunning)
            {

                isRunning = false; //Hareketi kes

                //Onunde engel var bekle ve yon degistir.
                StartCoroutine(WaitAndFlip());
            }
            else if (isRunning && !isWaiting) //Durdugum zaman girmemesi icin eger engel yoksa patrol yap.
            {

                isRunning = true;
                anim.SetBool("isRunning", true); //BU DEGISECEK YURUME OLACAK


                //Saga veya sola hareket
                if (isFacingRight)
                {
                    transform.Translate(speed * Time.deltaTime * Vector2.right);
                }
                else
                {
                    transform.Translate(speed * Time.deltaTime * Vector2.left);
                }
            }

        }
        #endregion



        //Hareket particle
        if (isRunning) //hareket halinde ise
        {
            StartCoroutine(SpawnRunParticle());
        }
        else
        {
            StopCoroutine(SpawnRunParticle());
            coroutineAllowed_running = true;
        }

    }


    #region Saldiri
    private IEnumerator Attack_1()
    {       

        yield return new WaitForSeconds(0.25f); //Hemen saldirma

        //Animasyon
        anim.SetTrigger("Attack1");

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

    }

    private IEnumerator Attack_2()
    {   

        yield return new WaitForSeconds(0.25f); //Hemen saldirma

        //Animasyon - 2
        anim.SetTrigger("Attack2");

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

    }

    private IEnumerator AttackWait()
    {       
        yield return new WaitForSeconds(1.3f);
        isAttacking = false;
    }
    #endregion

    #region Particle
    IEnumerator SpawnRunParticle()
    {
        if (coroutineAllowed_running)
        {
            coroutineAllowed_running = false;
            yield return new WaitForSeconds(0.4f);
            while (isRunning)
            {
                Instantiate(runParticle, groundCheck.transform.position, runParticle.transform.rotation);
                yield return new WaitForSeconds(0.4f);
            }
        }

    }

    IEnumerator SpawnCatchLogo()
    {
        if (isCatching)
        {
            isCatching = false;
            //Tespit edildi unlem isaretini cikar.
            var myObject = Instantiate(catchLogo, new Vector2(transform.position.x + rayDistanceCheckWay / 2.8f /*Su an degeri -1 ondan dolayi! */, transform.position.y + 3.2f), Quaternion.identity);
            myObject.transform.parent = gameObject.transform.GetChild(0).parent;
            yield return new WaitForSeconds(10);
            isCatching = true;
        }
        
    }

    #endregion


   
    //Sureli hareket
    private IEnumerator MovementFlip()
    {
        
        yield return new WaitForSeconds(Random.Range(6,12));

        if(!isFollowing && !isWaiting) //Takip etmiyorsa ve beklemiyorsa yani patrolde ise
        {
            //Biraz bekle ve don
            isRunning = false;
            anim.SetBool("isRunning", false); //Idle animasyonuna gec

            yield return new WaitForSeconds(Random.Range(3, 6));

            Flip();

            isRunning = true;
        }

        StartCoroutine(MovementFlip());
    }

    //Bekle
    private IEnumerator WaitAndFlip()
    {
        isWaiting = true;

        anim.SetBool("isRunning", false); //Idle animasyonuna gec

        yield return new WaitForSeconds(Random.Range(3, 7));

        if (!isFollowing) //Eger takip ettigin dusman varsa donme
            Flip(); //Don!


        isWaiting = false;
        isRunning = true;
    }


    //Flip
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

        //Raycast isinlarinin yonlerini de karakter ile birlikte degistir
        rayDistanceFront = -1 * rayDistanceFront;
        rayDistanceBack = -1 * rayDistanceBack;
        rayDistanceCheckWay = -1 * rayDistanceCheckWay;

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(rayStart.position, new Vector2(rayStart.position.x + rayDistanceFront, rayStart.position.y));
        Gizmos.DrawLine(new Vector2(rayStart.position.x, rayStart.position.y - 0.2f), new Vector2(rayStart.position.x + rayDistanceFront, rayStart.position.y - 2));
        Gizmos.DrawLine(rayStart_back.position, new Vector2(rayStart_back.position.x + rayDistanceBack, rayStart_back.position.y));
        Gizmos.DrawLine(rayStart_checkWay.position, new Vector2(rayStart_checkWay.position.x + rayDistanceCheckWay, rayStart_checkWay.position.y));

    }


}
