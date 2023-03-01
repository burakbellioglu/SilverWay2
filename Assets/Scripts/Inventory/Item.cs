
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Item/Yeni item olustur")]

public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public string tag;
    public Sprite icon;
}

