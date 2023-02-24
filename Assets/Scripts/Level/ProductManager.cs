using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProductManager : MonoBehaviour
{
    //Dukkanin icinde trigger ile paneli getirir
    //Paneldeki dukkan ogelerinin kontrolu
    //Seviyeye gore ayarlama buyuk panelde acma vs tum urun kontrolleri

    [Header("Objeleri")]
    public GameObject products;
    public Button exitButton;


    private void OnTriggerEnter2D(Collider2D collision) //Ekrana animasyon ile getir
    {
        if (collision.CompareTag("Player"))
        {
            products.SetActive(true);
            products.transform.parent.GetComponent<Animator>().SetTrigger("Get");
            exitButton.interactable = false;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            exitButton.interactable = true;
            StartCoroutine(CloseProducts_Delay());
        }
          
    }



    private IEnumerator CloseProducts_Delay()
    {
        Debug.Log("deneme");
        products.transform.parent.GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(1);
        products.SetActive(false);
    }

}
