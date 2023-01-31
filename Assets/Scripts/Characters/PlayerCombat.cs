using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{ 
    //AKT�F SORUNLAR-*-
    //Ayn� sald�r� t�r� bazen arka arkaya sald�rabiliyor.
    //Silahlara daha iyi collider yapmak i�in child objelerine collider eklenebilir.
    
    //Componentler
    private Animator anim;
    private PlayerController controller;

    //Yerel degiskenler
    string activeAttack;
    private bool isAttacking = false;

    //Degisken atamalar�
    public string[] LastAttacks = new string[2];
    public GameObject weaponSlot;

    [Header("Particle atamalar�")]
    public ParticleSystem[] cutParticles;
    public ParticleSystem[] thrustParticles;

    //NOT!!!!
    //1-Bicak, 2-Kilic, 3-Mizrak

    void Start()
    {

        anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (!isAttacking || LastAttacks[0] != "attack1")) //Attack 1
        {
            //Animasyon
            activeAttack = "attack1";
            anim.SetTrigger(activeAttack);

            //Karakterin y�z� ne tarafta? ona g�re particle y�nleri degisecek
            int deger;
            if (controller.isFacingRight)
                deger = 1;
            else
                deger = -1;


            //Particle           
            switch (FindChildWithTag("Weapon").tag) //Silah� bul ard�ndan colliderini bul ve hangi t�rse o tagi bul
            {

                case "Dagger":
                    var myObject = Instantiate(cutParticles[0], FindChildWithTag("Weapon").transform.parent.position, cutParticles[0].transform.rotation);                   
                    myObject.transform.parent = FindChildWithTag("Weapon").transform.parent;

                    //Olu�mu� partical objelerinin scale degerlerini duzelt (Baz� objelerin scale ile oynand�g� icin ayn� goruntu olusmyur).
                    myObject.transform.localScale = new Vector3(deger, 1, 1);

                    break;
                case "Sword":
                    var myObject1 = Instantiate(cutParticles[1], new Vector2(FindChildWithTag("Weapon").transform.parent.position.x, FindChildWithTag("Weapon").transform.parent.position.y + 2) , cutParticles[0].transform.rotation);
                    myObject1.transform.parent = FindChildWithTag("Weapon").transform.parent;

                    myObject1.transform.localScale = new Vector3(deger, 1, 1);
                    break;
                case "Spear":
                    var myObject2 = Instantiate(cutParticles[2], new Vector2(FindChildWithTag("Weapon").transform.parent.position.x, FindChildWithTag("Weapon").transform.parent.position.y + 2), cutParticles[0].transform.rotation);
                    myObject2.transform.parent = FindChildWithTag("Weapon").transform.parent;

                    myObject2.transform.localScale = new Vector3(deger, 1, 1);
                    break;
            }

           


            FindChildWithTag("Weapon").GetComponent<BoxCollider2D>().enabled = true; //Silah hasar verebilir.

            //Listeye son attack� atama
            LastAttacks[1] = LastAttacks[0];
            LastAttacks[0] = activeAttack;

            
            StartCoroutine(Attack()); //Ayn� sald�r� turu icin bekleme suresi.
        }

        if (Input.GetKeyDown(KeyCode.G) && (!isAttacking || LastAttacks[0] != "attack2")) //Attack 2
        {
            activeAttack = "attack2";
            anim.SetTrigger(activeAttack);

            //Karakterin y�z� ne tarafta? ona g�re particle y�nleri degisecek
            int deger;
            if (controller.isFacingRight)
                deger = 1;
            else
                deger = -1;

            //Particle           

            switch (FindChildWithTag("Weapon").tag) //Silaha g�re particle
            {
                case "Dagger":
                    var myObject = Instantiate(thrustParticles[0], FindChildWithTag("Weapon").transform.parent.position, thrustParticles[0].transform.rotation);
                    myObject.transform.parent = FindChildWithTag("Weapon").transform.parent;

                    myObject.transform.localScale = new Vector3(deger, 1, 1);
                    break;
                case "Sword":
                    var myObject1 = Instantiate(thrustParticles[1], new Vector2(FindChildWithTag("Weapon").transform.parent.position.x, FindChildWithTag("Weapon").transform.parent.position.y + 2), thrustParticles[0].transform.rotation);
                    myObject1.transform.parent = FindChildWithTag("Weapon").transform.parent;

                    myObject1.transform.localScale = new Vector3(deger, 1, 1);
                    break;
                case "Spear":
                    var myObject2 = Instantiate(thrustParticles[2], new Vector2(FindChildWithTag("Weapon").transform.parent.position.x, FindChildWithTag("Weapon").transform.parent.position.y + 2), thrustParticles[0].transform.rotation);
                    myObject2.transform.parent = FindChildWithTag("Weapon").transform.parent;

                    myObject2.transform.localScale = new Vector3(deger, 1, 1);
                    break;
            }



            FindChildWithTag("Weapon").GetComponent<BoxCollider2D>().enabled = true; //Silah hasar verebilir.

            LastAttacks[1] = LastAttacks[0];
            LastAttacks[0] = activeAttack;

            StartCoroutine(Attack()); //Ayn� sald�r� turu icin bekleme suresi.
        }

        if (Input.GetKeyDown(KeyCode.H) && (!isAttacking || LastAttacks[0] != "attack3")) //Attack 3
        {
            activeAttack = "attack3";
            anim.SetTrigger(activeAttack);

            //Karakterin y�z� ne tarafta? ona g�re particle y�nleri degisecek
            int deger;
            if (controller.isFacingRight)
                deger = 1;
            else
                deger = -1;

            var myObject = Instantiate(cutParticles[0], FindChildWithTag("Weapon").transform.parent.position, cutParticles[0].transform.rotation);
            myObject.transform.parent = FindChildWithTag("Weapon").transform.parent;

            myObject.transform.localScale = new Vector3(deger, 1, 1);

            LastAttacks[1] = LastAttacks[0];
            LastAttacks[0] = activeAttack;

            StartCoroutine(Attack()); //Ayn� sald�r� turu icin bekleme suresi.
        }

    }

    private GameObject FindChildWithTag(string tag) //Aktif silah�n Collider objesini bul
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

    IEnumerator Attack() //Sald�rmak icin beklet
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.65f);
        isAttacking = false;
    }

    public void FinishAttack() //Saldiriyi bitir collider kapat
    {
        FindChildWithTag("Weapon").GetComponent<BoxCollider2D>().enabled = false;
    }


}
