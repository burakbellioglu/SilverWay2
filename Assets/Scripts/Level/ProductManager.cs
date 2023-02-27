using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;


public class ProductManager : MonoBehaviour
{
    private PlayerStats playerStats;
    
    [Header("Atanacak UI Objeleri")]
    public Button exitButton;
    public GameObject buy_questionPanel;

    //Yerel degiskenler
    private GameObject selectedButton;
    //--Product bilgileri
    string _name;
    int _price;


    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    #region Panel Animasyonlarý

    public IEnumerator CloseProducts_Delay()
    {
        gameObject.transform.parent.GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    #endregion

    #region Panel Kontrolleri
    public void GetBuyPanel(int index)
    {
        selectedButton = EventSystem.current.currentSelectedGameObject.transform.gameObject;

        CloseSelectedImage();
        selectedButton.transform.Find("Selected").gameObject.SetActive(true);

        CloseBuyPanels();
        gameObject.transform.Find("BuyPanels").GetChild(index).gameObject.SetActive(true);
    }

    public void CloseBuyPanels() //Satýn alma panellerini kapat
    {
        for (int i = 0; i < gameObject.transform.Find("BuyPanels").childCount; i++)
        {
            gameObject.transform.Find("BuyPanels").GetChild(i).gameObject.SetActive(false);
        }
    }

    public void CloseSelectedImage() //Butonlarýn secilme resmini kapat
    {
        for (int i = 0; i < selectedButton.transform.parent.childCount; i++)
        {
            selectedButton.transform.parent.GetChild(i).Find("Selected").gameObject.SetActive(false);
        }
    }
    #endregion

    public void Buy()
    {
        //Buy butonuna tiklandiginda o urunun bilgilerini al
        GameObject product = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;

        int price = Convert.ToInt32(product.transform.Find("Price").GetChild(0).GetComponent<TextMeshProUGUI>().text);

        _name = product.name;
        _price = price;

        //Soru panelini goster

        //Soru panelindeki butonlara listener atama
        buy_questionPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(Buy_Question_Yes);
        buy_questionPanel.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(Buy_Question_No);

        buy_questionPanel.SetActive(true);
    }


    public void Buy_Question_Yes()
    {
        if (_price <= playerStats.coin) //Satýn al
        {
            playerStats.coin -= _price;
        }
        else //Para yetersiz
        {
            Debug.Log("Yetersiz Para");
        }

        buy_questionPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void Buy_Question_No()
    {
        buy_questionPanel.SetActive(false);

        buy_questionPanel.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
    }

}
