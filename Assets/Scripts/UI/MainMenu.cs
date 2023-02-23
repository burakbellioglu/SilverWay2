using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    //Once sola dogru hareket ettiren fonksiyon
    //bundan yaklasik 1 saniye sonra left panelin saydamlýk animasyonu
    //en son o panelin aktifligi acilip kapanmasi

    [Header("Atanacak objeler")]
    public GameObject leftPanel;
    public GameObject _camera;
    [SerializeField] private GameObject secilenPanel;

    [Header("Hava durumu")]
    public GameObject yagmurlu;
    public GameObject temiz;


    private void Start()
    {
        int rndm = Random.Range(0, 2);
        if(rndm == 0)
        {
            temiz.SetActive(false);
            yagmurlu.SetActive(true);           
        }
        else
        {
            yagmurlu.SetActive(false);
            temiz.SetActive(true);       
        }
    }

    #region Left panel animasyonlarý
    public void LeftPanel(GameObject secilen)
    {
        secilenPanel = secilen;

        StartCoroutine(LeftPanelYieldToLeft());
    }

    public void LeftPanelToRight()
    {
        StartCoroutine(LeftPanelYieldToRight());
    }

    public IEnumerator LeftPanelYieldToRight()
    {
        //Kapatma animasyonu
        //false yap

        _camera.GetComponent<Animator>().SetTrigger("Right");

        leftPanel.GetComponent<Animator>().SetTrigger("Close");

        yield return new WaitForSeconds(1);

        secilenPanel.SetActive(false);

        yield return new WaitForSeconds(1);

        leftPanel.SetActive(false);
    }

    public IEnumerator LeftPanelYieldToLeft() //Left panel animasyonu ve kontrolü
    {
        //true yap
        //Acma animasyonu
        leftPanel.SetActive(true);
        _camera.GetComponent<Animator>().SetTrigger("Left");

        yield return new WaitForSeconds(1);
        leftPanel.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.3f);

        secilenPanel.SetActive(true);

    }

    public void LeftPanelController()
    {
        if (secilenPanel.activeSelf) //Aktifse kapat
        {
            secilenPanel.SetActive(false);
        }
        else //Ac
        {
            secilenPanel.SetActive(true);
        }

    }
    #endregion


}
