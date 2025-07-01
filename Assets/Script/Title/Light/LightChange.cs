using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public Color[] colors;
    private int colorIndex = 0;
    private Light myLight;
    private bool isColor = true;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (colors.Length == 0) return;

            colorIndex = (colorIndex + 1) % colors.Length;
            myLight.color = colors[colorIndex];
            
        }
    }
}
    