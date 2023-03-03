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
    
    public void Selected()
    {
        if (gameObject.transform.Find("Selected").gameObject.activeSelf)
        {
            gameObject.transform.Find("Selected").gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.Find("Selected").gameObject.SetActive(true);
            StartCoroutine(CloseSelected());
        }
    }

    private IEnumerator CloseSelected()
    {
        yield return new WaitForSeconds(4);
        gameObject.transform.Find("Selected").gameObject.SetActive(false);
    }

    public void Equip() //Objeyi giy/kullan
    {

        if (gameObject.CompareTag("Weapon")) //Silah turunde ise
        {
            EquipManager.Instance.EquipWeapon(transform.parent.name);

            //Giyilen objeyi kaldir
            Item _item = (Item)AssetDatabase.LoadAssetAtPath("Assets/Items/" + transform.parent.name + ".asset", typeof(Item));
            InventoryManager.Instance.Remove(_item);

            //Bu objeyi silah kismina at ekipmanlarda
            InventoryManager.Instance.Ekipmanlar.Find("Weapon").Find("Icon").GetComponent<Image>().sprite = _item.icon;
            InventoryManager.Instance.Ekipmanlar.Find("Weapon").Find("Icon").GetComponent<Image>().color = new Color(255, 255, 255, 255);
            InventoryManager.Instance.Ekipmanlar.Find("Weapon").Find("Name").GetComponent<TextMeshProUGUI>().text = _item.itemName;

            //Bu objeyi yani ekipmani envanterden yok et.
            Destroy(transform.parent.gameObject);

        }
        else //Zirh ise
        {

        }

    }

}
