using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingStorm : MonoBehaviour
{
    //NOT!
    //Optimizasyona büyük etki ettiði için grafik seçimine göre scripti kaldýr!


    private Light2D lightComp;
    public float Speed;
    public float SpeedForGlobal;

    private Light2D globalLight_front;

    private float deger = 0; //Iþýða atama yapmak için ýþýðýn anlýk deðerini tutan bir deðiþken
    private float deger_ = 0.9f; //Global isik icin anlik deger tutan degisken.

    void Start()
    {
        lightComp = GetComponent<Light2D>();

        globalLight_front = GameObject.Find("Global Light").GetComponent<Light2D>();

        StartCoroutine(LightingRUN());
    }

    private IEnumerator LightingRUN()
    {
        yield return new WaitForSeconds(Random.Range(2,7));

        StartCoroutine(GlobalLightingEffect()); //Etrafý aydýnlatma kodu

        while (lightComp.intensity < 9) //Yýldýrýmý baþlatma döngüsü
        {
            int rndm = Random.Range(-2, 3); //Yýldýrým konumu için random sayý üret
            deger += Time.deltaTime * Speed; //Yeni Iþýk deðerini al
            transform.position = new Vector3(rndm + transform.position.x, transform.position.y,190); //Yeni pozisyona geç
            lightComp.intensity = deger; //Iþýk deðerini ata
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(0.3f);

        while (lightComp.intensity > 0) //Yýldýrým Sönme Döngüsü
        {
            deger -= Time.deltaTime * Speed; //Yeni Iþýk deðerini al
            lightComp.intensity = deger; //Iþýk deðerini ata
            yield return new WaitForSeconds(0.1f);
        }

        lightComp.intensity = 0;

       
        StartCoroutine(LightingRUN());
    }

    //Yýldýrým çaktýðýnda etrafýn aydýnlanmasý.
    private IEnumerator GlobalLightingEffect()
    {
      
        while (globalLight_front.intensity < 1.3f) //Isigin aydýnlanmasý.
        {       
            deger_ += Time.deltaTime * SpeedForGlobal; //Yeni Iþýk deðerini al

            globalLight_front.intensity = deger_; //Iþýk deðerini ata
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(0.3f);

        while (globalLight_front.intensity > 0.9f) //Isigi eski haline getirme dongusu
        {
            deger_ -= Time.deltaTime * SpeedForGlobal; //Yeni Iþýk deðerini al
            globalLight_front.intensity = deger_; //Iþýk deðerini ata
            yield return new WaitForSeconds(0.1f);
        }

        globalLight_front.intensity = 0.9f;

    }


}
