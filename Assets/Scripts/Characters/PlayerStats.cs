using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    //OPTIMIZE MI? BASKA NE YAPILABILIR
    //NORMALDEN STATÝCE ATAMA

    [Header("Degerler")]
    public int _level;
    public float _xp;
    public int _coin;

    //Static degerler
    public static float xp;
    public static int level;
    public static int coin;

    [Header("UI Atamalarý")]
    public GameObject StatsUI;

    public static TextMeshProUGUI money_text;
    public static TextMeshProUGUI level_text;

    private void Start()
    {
        level = _level;
        xp = _xp;
        coin = _coin;

        money_text = StatsUI.transform.Find("Money").Find("Text").GetComponent<TextMeshProUGUI>();
        level_text = StatsUI.transform.Find("Level").Find("Text").GetComponent<TextMeshProUGUI>();

        WriteStats();
    }

    public static void WriteStats()
    {
        money_text.text = coin.ToString();
        level_text.text = level.ToString();
    }

}
