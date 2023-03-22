using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class Equipment : MonoBehaviour
{

    //Silah veya zirh giyme olacak
    //Elimizde ismi ve resmi var
    //Switch case isim ile atama yapabiliriz silah icin

    //Local
    Item _item;

    public void Selected()
    {

        //Ortadaki info paneline o objenin bilgilerine gore paneli olustur (Equip Manager)
        InventoryManager.Instance.GetInfoPanel(name, gameObject.tag);

    }

    public void Equip() //Objeyi giy/kullan
    {
        InventoryManager.Instance.RemoveListItems();

        if (gameObject.CompareTag("Weapon")) //Silah turunde ise
        {
           
            //Giyilen objeyi kaldir            
            for (int i = 0; i < InventoryManager.Instance.Items.Count; i++)
            {
                if (name == InventoryManager.Instance.Items[i].name)
                   _item = InventoryManager.Instance.Items[i];
            }

            //Karakter uzerýnde silahi aktiflestir
            EquipManager.Instance.EquipWeapon(_item);

            InventoryManager.Instance.Remove(_item);
          
            //Bu objeyi yani ekipmani envanterden yok et.
            Destroy(gameObject);

        }
        else //Zirh ise
        {
            for (int i = 0; i < InventoryManager.Instance.Items.Count; i++) //o zirhi bul
            {
                if (name == InventoryManager.Instance.Items[i].name)
                    _item = InventoryManager.Instance.Items[i];
            }            

            EquipManager.Instance.EquipArmor(_item); //Karakterin uzerinde zirh aktiflessin ve koruma verilsin

            //Giyilen objeyi kaldir
            InventoryManager.Instance.Remove(_item);

            //Bu objeyi yani ekipmani envanterden yok et.
            Destroy(gameObject);
        }

        InventoryManager.Instance.ListItems();

    }

    public void Drop() //Objeyi býrak.
    {
        Debug.Log("Drop");
    }
}
