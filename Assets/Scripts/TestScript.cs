using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject high;
    public GameObject particles;
    public GameObject fogparticles;
    public GameObject raindropparticles;




    public void HighGraphic()
    {
        high.SetActive(true);
        Debug.Log(QualitySettings.names[5]);
        QualitySettings.SetQualityLevel(5, true);
    }

    public void LowGraphic()
    {
        high.SetActive(false);
        Debug.Log(QualitySettings.names[1]);
        QualitySettings.SetQualityLevel(1, true);
    }

    public void YagmurParticles()
    {
        if (particles.activeSelf)
            particles.SetActive(false);
        else
            particles.SetActive(true);
       
    }

    public void FogParticles()
    {
        if (fogparticles.activeSelf)
            fogparticles.SetActive(false);
        else
            fogparticles.SetActive(true);

    }

    public void RainDropParticles()
    {
        if (raindropparticles.activeSelf)
            raindropparticles.SetActive(false);
        else
            raindropparticles.SetActive(true);

    }

}
