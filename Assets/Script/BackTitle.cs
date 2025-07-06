using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class BackTitle : MonoBehaviour
{
    GamePadCommand command;
    int change;
    private bool isEsc = false;
    private RectTransform imageRect1;
    private RectTransform imageRect2;
    public void Start()
    {
        command = new GamePadCommand();
        change = 0;//現在の位置　0が上　１が下
        isEsc = false;      
        imageRect1 = transform.Find("M_BackGroundUI/M_CloseUI").GetComponent<RectTransform>();
        imageRect2 = transform.Find("M_BackGroundUI/M_QuitUI").GetComponent<RectTransform>();


        // UI初期化
        imageRect1.anchoredPosition = new Vector2(0, 100);
        imageRect1.sizeDelta = new Vector2(500, 150);
        imageRect2.anchoredPosition = new Vector2(0, -80);
        imageRect2.sizeDelta = new Vector2(400, 120);
        change = 0;
    }

    public void Update()
    {
        //ゲームパッドやキーボードで同じ処理ができるようなプログラムを取得
        command = new GamePadCommand();

        //ESCキーが押されるまでは以下の処理は入らない
        if (command.GetEscKey((int)GameManager.Instance.inputDevice))
        {
            isEsc = true;
            ShowPanel();
        }
    
        if(isEsc)
        {
            //上にUI移動
            if (command.UpAction((int)GameManager.Instance.inputDevice))
            {
                if (change == 1)
                {
                    // ContineuImageの位置とサイズ変更
                    // 位置を変更
                    imageRect1.anchoredPosition = new Vector2(0, 100);
                    // サイズを変更
                    imageRect1.sizeDelta = new Vector2(500, 150);
                    //　QuitImageの位置とサイズを変更
                    imageRect2.anchoredPosition = new Vector2(0, -80);
                    imageRect2.sizeDelta = new Vector2(400, 120);
                    change = 0;
                }
            }
            //下に移動
            if (command.DownAction((int)GameManager.Instance.inputDevice))
            {
                if (change == 0)
                {
                    imageRect2.anchoredPosition = new Vector2(0, -100);
                    imageRect2.sizeDelta = new Vector2(500, 150);

                    imageRect1.anchoredPosition = new Vector2(0, 80);
                    imageRect1.sizeDelta = new Vector2(400, 120);
                    change = 1;
                }
            }

            //＊＊＊＊＊＊＊＊＊＊＊＊＊決定後の処理＊＊＊＊＊＊＊＊＊＊＊＊
            if (command.IsBbutton((int)GameManager.Instance.inputDevice))
            {
                //それぞれのUIの位置野処理
                switch (change)
                {
                    //上の場合
                    case 0:
                        SceneManager.LoadScene("Title");
                        break;
                    //下の場合
                    case 1:
                        HidePanel();
                        break;
                }
            }
        }

        
    }
    // 表示関数
    void ShowPanel()
    {
        GameObject obj = GameObject.Find("Menu");

        Transform panel = obj.transform.Find("M_BackGroundUI");

        panel.gameObject.SetActive(true);
    }

    // 非表示関数
    void HidePanel()
    {
        GameObject obj = GameObject.Find("Menu");

        Transform panel = obj.transform.Find("M_BackGroundUI");

        panel.gameObject.SetActive(false);
    }
}
