using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance;

    public GameObject WeaponSlot;

    private void Awake()
    {
        Instance = this;
    }

    public void EquipWeapon(string itemName)
    {
        
        for (int i = 0; i < WeaponSlot.transform.childCount; i++) //Item name ile silahi bul ve aktiflestir / digerlerini kapat
        {
            if (WeaponSlot.transform.GetChild(i).name == itemName)
                WeaponSlot.transform.GetChild(i).gameObject.SetActive(true);
            else
                WeaponSlot.transform.GetChild(i).gameObject.SetActive(false);
        }

    }


}
