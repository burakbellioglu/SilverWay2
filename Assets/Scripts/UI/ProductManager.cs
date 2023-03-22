using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;


public class ProductManager : MonoBehaviour
{
    
    [Header("Atanacak Standart UI Objeleri")]
    public Button exitButton;
    public GameObject buy_questionPanel;

    [Header("Atanacak Referanslar")]
    public GameObject buyPanels;

    //Yerel degiskenler
    private GameObject selectedButton;
    //--Product bilgileri
    string _name;
    int _price;
    GameObject product;


    private void Start()
    {
        if(gameObject.name == "SilahUstasi")
            CheckPlayerLevel_Silahci();
    }

    private void CheckPlayerLevel_Silahci() //Oyuncunun leveline gore itemleri goster 
    {
        GameObject content = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;

        for (int i = 0; i < content.transform.childCount; i++)
        {
            if(PlayerStats.level >= Convert.ToInt32(content.transform.GetChild(i).Find("Xp").GetChild(0).GetComponent<TextMeshProUGUI>().text))
            {
                content.transform.GetChild(i).GetComponent<Button>().interactable = true;
                content.transform.GetChild(i).Find("Locked").gameObject.SetActive(false);
            }
            
        }
    }

    #region Panel Animasyonlarý

    public IEnumerator CloseProducts_Delay()
    {
        gameObject.transform.parent.GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(1);
        buyPanels.SetActive(false);
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

        buyPanels.transform.GetChild(index).gameObject.SetActive(true);
    }

    public void CloseBuyPanels() //Satýn alma panellerini kapat
    {
        for (int i = 0; i < buyPanels.transform.childCount; i++)
        {
            buyPanels.transform.GetChild(i).gameObject.SetActive(false);
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


    #region Zirh Ustasi Ozel

    public void GetProductsScroll(GameObject category) //Kategoriue basildiginda o kategoriyi ac ve xp kontrolu yap
    {
        GameObject content = category.transform.GetChild(0).GetChild(0).gameObject;

        for (int i = 0; i < content.transform.childCount; i++)
        {
            if (PlayerStats.level >= Convert.ToInt32(content.transform.GetChild(i).Find("Xp").GetChild(0).GetComponent<TextMeshProUGUI>().text))
            {
                content.transform.GetChild(i).GetComponent<Button>().interactable = true;
                content.transform.GetChild(i).Find("Locked").gameObject.SetActive(false);
            }

        }

        category.SetActive(true);
    }

    #endregion


    #region Satin alma islemleri
    public void Buy()
    {
        //Buy butonuna tiklandiginda o urunun bilgilerini al
        product = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;

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
        if (_price <= PlayerStats.coin) //Satýn al
        {
            PlayerStats.coin -= _price;
            PlayerStats.WriteStats();

            //Envantere o urunu ekle
            product.GetComponent<ItemAdder>().AddInventory();

            buy_questionPanel.SetActive(false);
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
    #endregion

}
