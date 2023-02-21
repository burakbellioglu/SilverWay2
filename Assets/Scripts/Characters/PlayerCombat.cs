using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{ 
    
    //Componentler
    private Animator anim;
    private PlayerController controller;

    //Yerel degiskenler
    public bool isAttacking = false; //Karakterin dönme hareketi ve hareket hizi icin kullanýlýr. belirli bir sure fight icinde hareketi yavaslat.  
    public GameObject weaponSlot;

    [Header("Particle atamalarý")]
    public ParticleSystem[] cutParticles;
    public ParticleSystem[] thrustParticles;
    public ParticleSystem[] comboParticles;
    public ParticleSystem[] comboSwordParticles;

    [Header("Kombo sistemi")]
    private bool attack1 = false;
    private bool attack2 = false;
    private bool isCombo = false;

    //NOT!!!!
    //1-Bicak, 2-Kilic, 3-Mizrak

    void Start()
    {

        anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        
    }

   
    void Update()
    {

        //1.Saldiri turu
        if (Input.GetKeyDown(KeyCode.F) &&  !isCombo && !attack1)
        {
            //Animasyon
            anim.SetTrigger("attack1");

            Attack();

            StartCoroutine(AttackDelay(1)); //Ayný saldýrý turu icin bekleme suresi.

            //Eger kombo vurusu ise yani 2. saldiridan sonra 0.5 saniye icerisinde ilk saldiriyi baslatmissa
            if (attack2)
            {             
                var myObject = Instantiate(comboParticles[0], transform.position, comboParticles[0].transform.rotation);
                myObject.transform.parent = transform;
                
                StartCoroutine(Combo("attack3")); //Attack 3 animasyonuna ait comboyu calistir.
            }

        }

        //2.Saldiri turu
        if (Input.GetKeyDown(KeyCode.G)  && !isCombo && !attack2)
        {
            //Animasyon
            anim.SetTrigger("attack2");

            Attack();

            StartCoroutine(AttackDelay(2)); //Ayný saldýrý turu icin bekleme suresi.

            //Eger kombo vurusu ise yani 1. saldiridan sonra 0.5 saniye icerisinde ilk saldiriyi baslatmissa
            if (attack1)
            {
                var myObject = Instantiate(comboParticles[1], transform.position, comboParticles[1].transform.rotation);
                myObject.transform.parent = transform;

                StartCoroutine(Combo("attack4")); //Attack 4 animasyonuna ait comboyu calistir.
            }

        }

    }

    #region Attack Fonksiyonlari

    private IEnumerator Combo(string attackName)
    {
        isCombo = true;

        yield return new WaitForSeconds(0.3f);

        //Animasyon
        anim.SetTrigger(attackName);
        Attack();
        StartCoroutine(AttackDelay(3)); //Ayný saldýrý turu icin bekleme suresi.

        var myObject = Instantiate(comboSwordParticles[0], FindChildWithTag("Weapon").transform.parent.position, comboSwordParticles[0].transform.rotation);
        myObject.transform.parent = FindChildWithTag("Weapon").transform.parent;


        yield return new WaitForSeconds(1);
        isCombo = false;
    }

    private void Attack() //Particle olustur ve silahin colliderini aktiflestir.
    {
        //Karakterin yüzü ne tarafta? ona göre particle yönu belirlenecek.
        int deger;
        if (controller.isFacingRight)
            deger = 1;
        else
            deger = -1;

        //Particle           
        SpawnParticle(deger);

        FindChildWithTag("Weapon").GetComponent<BoxCollider2D>().enabled = true; //Silah hasar verebilir.
    }

    private GameObject FindChildWithTag(string tag) //Aktif silahýn Collider objesini bul
    {
        GameObject child = null;

        GameObject parent = weaponSlot;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag) && transform.gameObject.activeSelf)
            {
                child = transform.gameObject;
                break;
            }
        }

        return child.transform.Find("Collider").gameObject;
    }

    IEnumerator AttackDelay(int attackNo) //Saldýrmak icin beklet ve hangi attack ise atamasini yap.
    {
        switch (attackNo)
        {
            case 1:
                attack1 = true;
                break;
            case 2:
                attack2 = true;
                break;
        }

        isAttacking = true;

        yield return new WaitForSeconds(0.30f);

        isAttacking = false;

        yield return new WaitForSeconds(0.35f);

        switch (attackNo)
        {
            case 1:
                attack1 = false;
                break;
            case 2:
                attack2 = false;
                break;
        }

       

    }

    private void SpawnParticle(int deger)
    {
        switch (FindChildWithTag("Weapon").tag) //Silahý bul ardýndan colliderini bul ve hangi türse o tagi bul
        {

            case "Dagger":
                var myObject = Instantiate(cutParticles[0], FindChildWithTag("Weapon").transform.parent.position, cutParticles[0].transform.rotation);
                myObject.transform.parent = FindChildWithTag("Weapon").transform.parent;

                //Oluþmuþ partical objelerinin scale degerlerini duzelt (Bazý objelerin scale ile oynandýgý icin ayný goruntu olusmyur).
                myObject.transform.localScale = new Vector3(deger, 1, 1);

                break;
            case "Sword":
                var myObject1 = Instantiate(cutParticles[1], new Vector2(FindChildWithTag("Weapon").transform.parent.position.x, FindChildWithTag("Weapon").transform.parent.position.y + 2), cutParticles[0].transform.rotation);
                myObject1.transform.parent = FindChildWithTag("Weapon").transform.parent;

                myObject1.transform.localScale = new Vector3(deger, 1, 1);
                break;
            case "Spear":
                var myObject2 = Instantiate(cutParticles[2], new Vector2(FindChildWithTag("Weapon").transform.parent.position.x, FindChildWithTag("Weapon").transform.parent.position.y + 2), cutParticles[0].transform.rotation);
                myObject2.transform.parent = FindChildWithTag("Weapon").transform.parent;

                myObject2.transform.localScale = new Vector3(deger, 1, 1);
                break;
        }
    }

    public void FinishAttack() //Saldiriyi bitir collider kapat
    {
        FindChildWithTag("Weapon").GetComponent<BoxCollider2D>().enabled = false;
    }

    #endregion






}
