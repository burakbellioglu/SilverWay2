using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

            ////Giyilen objeyi kaldir
            //Item _item = (Item)AssetDatabase.LoadAssetAtPath("Assets/Items/" + transform.parent.name + ".asset", typeof(Item));

            //InventoryManager.Instance.Remove(_item);

            ////Bu scripti yok et
            //Destroy(transform.parent.gameObject);
        }
        else //Zirh ise
        {

        }

    }

}
