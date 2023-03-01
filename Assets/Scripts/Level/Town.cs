using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    public GameObject Kotu;
    public GameObject Temiz;
    public GameObject Yagmur;

    [Header("Otomatik Hava durumu")]
    public bool HavaDurumu;

    private void Awake()
    {
        if (HavaDurumu)
        {
            int rndm = Random.Range(0, 2);

            switch (rndm)
            {
                case 0: //Temiz hava
                    Kotu.SetActive(false);
                    Yagmur.SetActive(false);
                    Temiz.SetActive(true);

                    GameObject[] lights = GameObject.FindGameObjectsWithTag("Light");

                    foreach (var light in lights)
                    {
                        light.SetActive(false);
                    }

                    break;

                case 1://Yagmurlu hava

                    int _rndm = Random.Range(0, 2);

                    switch (_rndm)
                    {
                        case 0: //Hafif yagmur
                            Yagmur.transform.Find("Cise").gameObject.SetActive(true);

                            GameObject[] _lights = GameObject.FindGameObjectsWithTag("Light");

                            foreach (var light in _lights)
                            {
                                light.SetActive(false);
                            }
                            break;
                        case 1: //Saganak
                            Yagmur.transform.Find("Saganak").gameObject.SetActive(true);
                            break;
                    }

                    break;
            }
        }
       

    }


}
