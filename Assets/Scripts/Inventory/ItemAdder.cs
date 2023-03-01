using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdder : MonoBehaviour
{
    public Item item_;


    public void AddInventory() //Envantere ekle
    {
       InventoryManager.Instance.Add(item_);
    }
}
