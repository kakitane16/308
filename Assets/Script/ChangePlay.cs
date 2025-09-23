using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlay : MonoBehaviour
{
    public Image playing;

    public Sprite shot;
    public Sprite power;
    public Sprite angle;

    
    public void ChangeUI(int i)
    {
        playing.rectTransform.anchoredPosition = new Vector2(600.0f, 350.0f);
        playing.rectTransform.sizeDelta = new Vector2(400.0f, 300.0f);
        switch (i)
        {
            case 0:
                playing.sprite = angle;
                break;
            case 1:
                playing.sprite = power;
                break;
            case 2:
                playing.sprite = shot;
                break;
        }

    }
}
