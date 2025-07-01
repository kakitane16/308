using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLight : MonoBehaviour
{
    public Color[] colors;
    private int colorIndex = 0;
    private Light myLight;

    public float changeInterval = 2.0f; // F‚ğ•Ï‚¦‚éŠÔŠui•bj
    private float timer = 0f;

    void Start()
    {
        myLight = GetComponent<Light>();
        if (colors.Length > 0)
        {
            myLight.color = colors[0];
        }
    }

    void Update()
    {
        if (colors.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;
            colorIndex = (colorIndex + 1) % colors.Length;
            myLight.color = colors[colorIndex];
        }
    }
}
