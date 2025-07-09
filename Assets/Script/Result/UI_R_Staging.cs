using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_R_Staging : MonoBehaviour
{
    public Sprite[] Sprites;//表示する画像
    public Image[] ImageSlots;
    public GameObject ButtonL;
    public GameObject ButtonR;
    public float Delay = 1.0f;
    public float ButtonDelay = 1.0f;

    private int CurrentIndex = 0;
    private float timer = 0.0f;
    private bool isAllImages = false;
    private float ButtonTimer = 0.0f;

    void Start()
    {
        ButtonL.SetActive(false);
        ButtonR.SetActive(false);
    }

    void Update()
    {
        if (!isAllImages)
        {
            timer += Time.deltaTime;
            if (timer >= Delay && CurrentIndex < Sprites.Length && CurrentIndex < ImageSlots.Length)
            {
                timer = 0.0f;

                //ImageにSpriteをセット
                ImageSlots[CurrentIndex].sprite = Sprites[CurrentIndex];
                ImageSlots[CurrentIndex].gameObject.SetActive(true);

                CurrentIndex++;

                //画像が全て表示されたら
                if (CurrentIndex >= Sprites.Length) isAllImages = true;
            }
        }
        else//最後の画像を表示したらボタンを表示
        {
            ButtonTimer += Time.deltaTime;
            if (ButtonTimer >= ButtonDelay)
            {
                if(ButtonL != null && !ButtonL.activeSelf) ButtonL.SetActive(true);
                if(ButtonR != null && !ButtonR.activeSelf) ButtonR.SetActive(true);
            }
        }

    }
}