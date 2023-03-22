using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance;

    [Header("Silah Slot Atamalari")]
    public GameObject WeaponSlot;

    [Header("Zirh Sprite Atamalari")]
    public SpriteRenderer helmet;
    public SpriteRenderer hat;
    public SpriteRenderer body;
    public SpriteRenderer[] shoulders;
    public SpriteRenderer[] arms;
    public SpriteRenderer[] hands;
    public SpriteRenderer hip;
    public SpriteRenderer[] thighs;
    public SpriteRenderer[] legs;
    public SpriteRenderer[] foots;


    [Header("Envanter Slot Atamalari")]
    public Image weaponSlot;
    public Image helmetSlot;
    public Image bodySlot;
    public Image shoulderSlot;
    public Image armSlot;
    public Image handSlot;
    public Image hipSlot;
    public Image thighSlot;
    public Image legSlot;
    public Image footSlot;



    private void Awake()
    {
        Instance = this;
    }

    public void EquipWeapon(Item item)
    {
        
        for (int i = 0; i < WeaponSlot.transform.childCount; i++) //Item name ile silahi bul ve aktiflestir / digerlerini kapat
        {
            if (WeaponSlot.transform.GetChild(i).name == item.name)
                WeaponSlot.transform.GetChild(i).gameObject.SetActive(true);
            else
                WeaponSlot.transform.GetChild(i).gameObject.SetActive(false);
        }

        if (weaponSlot.gameObject.activeSelf) //Eger icon aktifse yani item varsa
            InventoryManager.Instance.Add(weaponSlot.transform.parent.GetComponent<ItemAdder>().item_);


        weaponSlot.gameObject.SetActive(true);
        weaponSlot.transform.parent.GetChild(1).gameObject.SetActive(false);
        weaponSlot.sprite = item.icon;
        //Itemi ver
        weaponSlot.transform.parent.GetComponent<ItemAdder>().item_ = item;

    }

    public void EquipArmor(Item item)
    {
        //Sprite degistir ve itemin korumasýný ver
        //Atama ile sprite bolgelerini al
        //switch case ile hangi bolge oldugunu anla
        //item ozelligi olarak bolge ismi ver
        //Envanterde olustur

        switch (item.target) //Itemi giy
        {
            case "Helmet":

                if (helmetSlot.gameObject.activeSelf) //Eger icon aktifse yani item varsa
                    InventoryManager.Instance.Add(helmetSlot.transform.parent.GetComponent<ItemAdder>().item_);


                //Iconu degistir ve aktiflestir, sembolu kaldir.
                helmetSlot.gameObject.SetActive(true);
                helmetSlot.transform.parent.GetChild(1).gameObject.SetActive(false);
                helmetSlot.sprite = item.icon;
                //Itemi ver
                helmetSlot.transform.parent.GetComponent<ItemAdder>().item_ = item;


                if (item.name == "KraliyetKaski")//Su anlik 1 adet kask var bu degisebilir
                {
                    helmet.sprite = item.sprite;
                    helmet.transform.GetChild(0).gameObject.SetActive(false); //Saci kapat
                }
                else
                    hat.sprite = item.icon;

                break;

            case "Body":
                body.sprite = item.sprite;

                if (bodySlot.gameObject.activeSelf) //Eger icon aktifse yani item varsa
                    InventoryManager.Instance.Add(bodySlot.transform.parent.GetComponent<ItemAdder>().item_);

                bodySlot.gameObject.SetActive(true);
                bodySlot.transform.parent.GetChild(1).gameObject.SetActive(false);
                bodySlot.sprite = item.icon;
                bodySlot.transform.parent.GetComponent<ItemAdder>().item_ = item;

                break;

            case "Shoulder":
                shoulders[0].sprite = item.sprite;
                shoulders[1].sprite = item.sprite;

                if (shoulderSlot.gameObject.activeSelf) //Eger icon aktifse yani item varsa
                    InventoryManager.Instance.Add(shoulderSlot.transform.parent.GetComponent<ItemAdder>().item_);

                shoulderSlot.gameObject.SetActive(true);
                shoulderSlot.transform.parent.GetChild(1).gameObject.SetActive(false);
                shoulderSlot.sprite = item.icon;
                shoulderSlot.transform.parent.GetComponent<ItemAdder>().item_ = item;

                break;

            case "Arm":
                arms[0].sprite = item.sprite;
                arms[1].sprite = item.sprite;

                if (armSlot.gameObject.activeSelf) //Eger icon aktifse yani item varsa
                    InventoryManager.Instance.Add(armSlot.transform.parent.GetComponent<ItemAdder>().item_);

                armSlot.gameObject.SetActive(true);
                armSlot.transform.parent.GetChild(1).gameObject.SetActive(false);
                armSlot.sprite = item.icon;
                armSlot.transform.parent.GetComponent<ItemAdder>().item_ = item;

                break;

            case "Hand":
                hands[0].sprite = item.sprite;
                hands[1].sprite = item.sprite;

                if (handSlot.gameObject.activeSelf) //Eger icon aktifse yani item varsa
                    InventoryManager.Instance.Add(handSlot.transform.parent.GetComponent<ItemAdder>().item_);

                handSlot.gameObject.SetActive(true);
                handSlot.transform.parent.GetChild(1).gameObject.SetActive(false);
                handSlot.sprite = item.icon;
                handSlot.transform.parent.GetComponent<ItemAdder>().item_ = item;

                break;

            case "Hip":
                hip.sprite = item.sprite;

                if (hipSlot.gameObject.activeSelf) //Eger icon aktifse yani item varsa
                    InventoryManager.Instance.Add(hipSlot.transform.parent.GetComponent<ItemAdder>().item_);

                hipSlot.gameObject.SetActive(true);
                hipSlot.transform.parent.GetChild(1).gameObject.SetActive(false);
                hipSlot.sprite = item.icon;
                hipSlot.transform.parent.GetComponent<ItemAdder>().item_ = item;

                break;

            case "Thigh":
                thighs[0].sprite = item.sprite;
                thighs[1].sprite = item.sprite;

                if (thighSlot.gameObject.activeSelf) //Eger icon aktifse yani item varsa
                    InventoryManager.Instance.Add(thighSlot.transform.parent.GetComponent<ItemAdder>().item_);

                thighSlot.gameObject.SetActive(true);
                thighSlot.transform.parent.GetChild(1).gameObject.SetActive(false);
                thighSlot.sprite = item.icon;
                thighSlot.transform.parent.GetComponent<ItemAdder>().item_ = item;

                break;

            case "Leg":
                legs[0].sprite = item.sprite;
                legs[1].sprite = item.sprite;

                if (legSlot.gameObject.activeSelf) //Eger icon aktifse yani item varsa
                    InventoryManager.Instance.Add(legSlot.transform.parent.GetComponent<ItemAdder>().item_);

                legSlot.gameObject.SetActive(true);
                legSlot.transform.parent.GetChild(1).gameObject.SetActive(false);
                legSlot.sprite = item.icon;
                legSlot.transform.parent.GetComponent<ItemAdder>().item_ = item;

                break;

            case "Foot":
                foots[0].sprite = item.sprite;
                foots[1].sprite = item.sprite;

                if (footSlot.gameObject.activeSelf) //Eger icon aktifse yani item varsa
                    InventoryManager.Instance.Add(footSlot.transform.parent.GetComponent<ItemAdder>().item_);

                footSlot.gameObject.SetActive(true);
                footSlot.transform.parent.GetChild(1).gameObject.SetActive(false);
                footSlot.sprite = item.icon;
                footSlot.transform.parent.GetComponent<ItemAdder>().item_ = item;

                break;

        }

        //Karaktere o itemin korumasi kadar koruma ver


    }


}
