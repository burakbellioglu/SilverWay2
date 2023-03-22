
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Item/Yeni item olustur")]

public class Item : ScriptableObject
{
    public string itemName;
    public int value;
    public string tag;
    public string target;
    public Sprite icon;
    public Sprite sprite;
    public int feature;
}

