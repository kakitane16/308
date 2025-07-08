using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BackTitle : MonoBehaviour
{
    public MenuUI menuUI;
   // public
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
            menuUI.ShowPanel();
        }

        if (isEsc)
        {
            //上に移動
            if (command.UpAction((int)GameManager.Instance.inputDevice))
            {
                if (change == 1)
                {
                    change = 0;
                    menuUI.SelectUI(change);
                }
            }
            //下に移動
            if (command.DownAction((int)GameManager.Instance.inputDevice))
            {
                if (change == 0)
                {
                    change = 1;
                    menuUI.SelectUI(change);
                }
            }
            //＊＊＊＊＊＊＊＊＊＊＊＊＊決定後の処理＊＊＊＊＊＊＊＊＊＊＊＊
            if (command.IsBbutton((int)GameManager.Instance.inputDevice))
            {
                switch (change)
                {
                    //上の場合
                    case 0:
                        SceneManager.LoadScene("Title");
                        isEsc = false;
                        break;
                    //下の場合
                    case 1:
                        menuUI.HidePanel();
                        isEsc = false;
                        break;
                }
            }
        }
          
    }


}
