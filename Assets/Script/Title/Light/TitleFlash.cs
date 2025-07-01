using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFlash : MonoBehaviour
{
    private float lightStrength;
    private bool maxStrength;
    private Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
        lightStrength = 0;
        maxStrength = false;
        myLight.intensity = lightStrength;
    }

    void Update()
    {
            myLight.intensity = lightStrength;

            if (lightStrength >= 5)
            {
                maxStrength = true;
            }
            else if (lightStrength <= 0)
            {
                maxStrength = false;
            }

            if (maxStrength)
            {
                lightStrength -= 3 * Time.deltaTime;
            }
            else
            {
                lightStrength += 3 * Time.deltaTime;
            }
        }
   
    }
