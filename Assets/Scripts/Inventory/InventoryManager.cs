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
    public Transform Ekipmanlar;

    [Header("Item prefableri")]
    public GameObject InventoryItem;
    public GameObject InventoryWeaponItem;
    public GameObject InventoryArmorItem;

    private void Awake()
    {
        Instance = this;
    }

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
                var _name = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                var _icon = obj.transform.Find("Icon").GetComponent<Image>();

                _name.text = item.itemName;
                _icon.sprite = item.icon;

                obj.name = item.name;
            }
            else if(item.tag == "Armor") //Zirh ise
            {
                GameObject obj = Instantiate(InventoryArmorItem, ItemContent);
                var _name = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                var _icon = obj.transform.Find("Icon").GetComponent<Image>();

                _name.text = item.itemName;
                _icon.sprite = item.icon;
            }
            else //Kusanilmayan normal bir item ise
            {
                GameObject obj = Instantiate(InventoryItem, ItemContent);
                var _name = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                var _icon = obj.transform.Find("Icon").GetComponent<Image>();

                _name.text = item.itemName;
                _icon.sprite = item.icon;
            }           

        }
    }

    public void RemoveListItems()
    {
        for (int i = 0; i < ItemContent.transform.childCount; i++)
        {
           Destroy(ItemContent.transform.GetChild(i).gameObject);
        }
    }

}
