using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public Transform InfoPanel;

    [Header("Item prefableri")]
    public GameObject InventoryItem;
    public GameObject InventoryWeaponItem;
    public GameObject InventoryArmorItem;

    [Header("UI prefableri")]
    public GameObject ItemInfo_panel;
    public GameObject ItemWeaponInfo_panel;
    public GameObject ItemArmorInfo_panel;

    [Header("UI objeleri")]
    public Button equip;
    public Button drop;

    //Yerel Degiskenler
    Item myItem;


    private void Awake()
    {
        Instance = this;
    }

    #region Ekleme cikarma ve listeleme islemleri
 
    public void Add(Item item)
    {
        Items.Add(item);

    }

    public void Remove(Item item)
    {
        Items.Remove(item);

    }

    public void ListItems()
    {
        foreach (var item in Items)
        {
            if(item.tag == "Weapon") //Silah ise
            {
                GameObject obj = Instantiate(InventoryWeaponItem, ItemContent);
                //var _name = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                var _icon = obj.transform.Find("Icon").GetComponent<Image>();

                //_name.text = item.itemName;
                _icon.sprite = item.icon;

                obj.name = item.name;
            }
            else if(item.tag == "Armor") //Zirh ise
            {
                GameObject obj = Instantiate(InventoryArmorItem, ItemContent);
                //var _name = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                var _icon = obj.transform.Find("Icon").GetComponent<Image>();

               // _name.text = item.itemName;
                _icon.sprite = item.icon;

                obj.name = item.name;
            }
            else //Kusanilmayan normal bir item ise
            {
                GameObject obj = Instantiate(InventoryItem, ItemContent);
                //var _name = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                var _icon = obj.transform.Find("Icon").GetComponent<Image>();

                //_name.text = item.itemName;
                _icon.sprite = item.icon;
            }           

        }

        equip.interactable = false;
        drop.interactable = false;

        equip.onClick.RemoveAllListeners();
        drop.onClick.RemoveAllListeners();
    }

    public void RemoveListItems()
    {
        for (int i = 0; i < ItemContent.transform.childCount; i++)
        {
           Destroy(ItemContent.transform.GetChild(i).gameObject);
        }

        if (InfoPanel.transform.childCount > 0)
            Destroy(InfoPanel.transform.GetChild(0).gameObject);
    }

    #endregion


    public void GetInfoPanel(string itemName, string itemTag) //Itemi bul ve veriler ile birlikte info panelini olustur.
    {

        //Once acik olan item var mý kontrol et
        if (InfoPanel.transform.childCount > 0)
        {            
            Destroy(InfoPanel.transform.GetChild(0).gameObject);
        }
            

        for (int i = 0; i < Items.Count; i++)  //Item bulundu.
        {
            if(Items[i].name == itemName) 
            {
                myItem = Items[i];
                break;
            }
        }

        //Buton ayarlamalari
        equip.interactable = true;
        drop.interactable = true;

        switch (itemTag)
        {
            case "Weapon":

                GameObject obj = Instantiate(ItemWeaponInfo_panel, InfoPanel);
                obj.name = itemName;

                obj.transform.Find("Flame").Find("Icon").GetComponent<Image>().sprite = myItem.icon;
                obj.transform.Find("Flame").Find("Name").GetComponent<TextMeshProUGUI>().text = myItem.itemName;

                obj.transform.Find("Degerler").Find("Damage").GetComponent<TextMeshProUGUI>().text = myItem.feature.ToString();
                obj.transform.Find("Degerler").Find("Value").GetComponent<TextMeshProUGUI>().text = (myItem.value * 0.9).ToString() + " - " + (myItem.value * 1.1).ToString();

                break;

            case "Armor":

                GameObject obj1 = Instantiate(ItemArmorInfo_panel, InfoPanel);
                obj1.name = itemName;

                obj1.transform.Find("Flame").Find("Icon").GetComponent<Image>().sprite = myItem.icon;
                obj1.transform.Find("Flame").Find("Name").GetComponent<TextMeshProUGUI>().text = myItem.itemName;

                obj1.transform.Find("Degerler").Find("Protect").GetComponent<TextMeshProUGUI>().text = myItem.feature.ToString();
                obj1.transform.Find("Degerler").Find("Value").GetComponent<TextMeshProUGUI>().text = (myItem.value * 0.8).ToString() + " - " + (myItem.value * 1.2).ToString();

                break;

            case "Item":

                GameObject obj2 = Instantiate(ItemInfo_panel, InfoPanel);
                obj2.name = itemName;

                obj2.transform.Find("Flame").Find("Icon").GetComponent<Image>().sprite = myItem.icon;
                obj2.transform.Find("Flame").Find("Name").GetComponent<TextMeshProUGUI>().text = myItem.itemName;

                obj2.transform.Find("Degerler").Find("Value").GetComponent<TextMeshProUGUI>().text = (myItem.value * 0.8).ToString() + " - " + (myItem.value * 1.2).ToString();

                equip.interactable = false;
                break;
        } //Olustur ve ozellikleri yazdir.


        //Butonlara listener atama Yok etmemize ragmen hala child olarak gordugu icin dongude atama yap
        for (int i = 0; i < InfoPanel.childCount; i++)
        {
            equip.onClick.AddListener(InfoPanel.GetChild(i).GetComponent<Equipment>().Equip);
            drop.onClick.AddListener(InfoPanel.GetChild(i).GetComponent<Equipment>().Drop);
        }
        

    }


}
