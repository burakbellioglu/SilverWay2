using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingStorm : MonoBehaviour
{
    //NOT!
    //Optimizasyona b�y�k etki etti�i i�in grafik se�imine g�re scripti kald�r!


    private Light2D lightComp;
    public float Speed;
    public float SpeedForGlobal;

    private Light2D globalLight_front;

    private float deger = 0; //I���a atama yapmak i�in �����n anl�k de�erini tutan bir de�i�ken
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

        StartCoroutine(GlobalLightingEffect()); //Etraf� ayd�nlatma kodu

        while (lightComp.intensity < 9) //Y�ld�r�m� ba�latma d�ng�s�
        {
            int rndm = Random.Range(-2, 3); //Y�ld�r�m konumu i�in random say� �ret
            deger += Time.deltaTime * Speed; //Yeni I��k de�erini al
            transform.position = new Vector3(rndm + transform.position.x, transform.position.y,190); //Yeni pozisyona ge�
            lightComp.intensity = deger; //I��k de�erini ata
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(0.3f);

        while (lightComp.intensity > 0) //Y�ld�r�m S�nme D�ng�s�
        {
            deger -= Time.deltaTime * Speed; //Yeni I��k de�erini al
            lightComp.intensity = deger; //I��k de�erini ata
            yield return new WaitForSeconds(0.1f);
        }

        lightComp.intensity = 0;

       
        StartCoroutine(LightingRUN());
    }

    //Y�ld�r�m �akt���nda etraf�n ayd�nlanmas�.
    private IEnumerator GlobalLightingEffect()
    {
      
        while (globalLight_front.intensity < 1.3f) //Isigin ayd�nlanmas�.
        {       
            deger_ += Time.deltaTime * SpeedForGlobal; //Yeni I��k de�erini al

            globalLight_front.intensity = deger_; //I��k de�erini ata
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(0.3f);

        while (globalLight_front.intensity > 0.9f) //Isigi eski haline getirme dongusu
        {
            deger_ -= Time.deltaTime * SpeedForGlobal; //Yeni I��k de�erini al
            globalLight_front.intensity = deger_; //I��k de�erini ata
            yield return new WaitForSeconds(0.1f);
        }

        globalLight_front.intensity = 0.9f;

    }


}
