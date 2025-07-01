using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
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
        if (Input.GetKey(KeyCode.Space)) // © ’·‰Ÿ‚µ‘Î‰ž
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
                lightStrength -= 5 * Time.deltaTime;
            }
            else
            {
                lightStrength += 5 * Time.deltaTime;
            }
        }
        else
        {
            lightStrength = 0;
            myLight.intensity = 0;
            maxStrength = false;
        }
    }
}
