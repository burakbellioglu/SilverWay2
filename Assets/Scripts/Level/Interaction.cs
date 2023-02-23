using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interaction : MonoBehaviour
{
    //Amac
    //Etkilesim
    //Collider ile alana girdiginde animasyon calissin ve buttonlar aktif olsun
    //Gorev veya etkilesim oldugu belli olmasi icin.

    //Lambaya idle animasyonu

    private Animator anim;
    private GameObject player;

    

    [Header("Objeler")]
    public Button button;
    public GameObject icerisi;

    [Header("Kameralar")]
    public GameObject playerCamera;

    [Header("UI")]
    public GameObject blackImage;

    [Header("Hava durumu")]
    public GameObject yagmurlu;
    public GameObject hafifYagmurlu;

    //Siyah ekran 2 saniue


    private void Start()
    {
        anim = gameObject.transform.Find("Animation").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact() //Iceriyi giris
    {
        //Artýk butona tiklama
        button.interactable = false;

        StartCoroutine(Interact_Delay());         
    }

    public IEnumerator Interact_Delay()
    {
        //Siyah ekran
        blackImage.SetActive(true);
        blackImage.GetComponent<Animator>().SetTrigger("Get");
        yield return new WaitForSeconds(1.5f);
        blackImage.GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(0.5f);
        blackImage.SetActive(false);

        

        //Gerekli Duzenlemeler
        playerCamera.SetActive(false);

        icerisi.SetActive(true);

        if (yagmurlu.activeSelf || hafifYagmurlu.activeSelf) //Eger disarida yagmur varsa yagmur particle aktiflestir
            icerisi.transform.Find("Rain").gameObject.SetActive(true);
        else
            yield return null;

        player.transform.position = icerisi.transform.Find("Player_TP_Point").position;

        yield return new WaitForSeconds(1);      
        button.interactable = true;
        button.onClick.RemoveListener(Interact);
        button.onClick.AddListener(InteractExit);

    }

    public void InteractExit() //Cikis
    {
        //Artýk butona tiklama
        button.interactable = false;

        StartCoroutine(InteractExit_Delay());      
    }

    private IEnumerator InteractExit_Delay()
    {
        //Siyah ekran
        blackImage.SetActive(true);
        blackImage.GetComponent<Animator>().SetTrigger("Get");
        yield return new WaitForSeconds(1.5f);
        blackImage.GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(0.5f);
        blackImage.SetActive(false);

        //Gerekli Duzenlemeler

        icerisi.SetActive(false);
        player.transform.position = gameObject.transform.Find("Player_TP_Point").position;

        playerCamera.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Animasyonlar Child
            anim.SetBool("Active", true);
            button.interactable = true;
            button.onClick.AddListener(Interact);            
            //Button
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Animasyonlar Child
            anim.SetBool("Active", false);
            button.interactable = false;
        }
    }

}
