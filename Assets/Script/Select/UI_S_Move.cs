using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_S_Move : MonoBehaviour
{
    public RectTransform content;       // 動かすUI（Panel_Content）
    public int MaxPage;                 // ページ数 - 1（例：3ページなら2）
    public float PageWidth;             // 1ページの幅
    public float SlideSpeed;            // 移動速度
    public float PageCoolTime;          //ページのクールタイム
    private Vector2 TargetPosition;     // 移動先の位置
    private int CurrentPage;            // 現在のページ
    float PageCoolTimeCount;            //クールタイムカウント用
    //コントローラー
    private GamePadCommand _command;
    private int GetInputOB;
    //矢印の表示管理
    public Image LeftArrow;
    public Image RightArrow;


    void Start()
    {
        //canvasのPos
        TargetPosition = content.anchoredPosition;

        //Padの初期化
        _command = new GamePadCommand();
        GetInputOB = (int)GameManager.Instance.inputDevice;
    }


    void Update()
    {
        //canvasのPos移動
        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, TargetPosition, Time.deltaTime * SlideSpeed);

        //コントローラー入力
        if (PageCoolTimeCount >= 5.0f)
        {
            //ページの移動
            if (_command.LeftAction(GetInputOB))
            {
                if (CurrentPage > 0)
                {
                    CurrentPage--;
                    TargetPosition = new Vector2(-PageWidth * CurrentPage, 0);
                    PageCoolTimeCount = 0.0f;
                }
            }
            if (_command.RightAction(GetInputOB))
            {
                if (CurrentPage < MaxPage)
                {
                    CurrentPage++;
                    TargetPosition = new Vector2(-PageWidth * CurrentPage, 0);
                    PageCoolTimeCount = 0.0f;
                }
            }

            //決定ボタン入力
            if (_command.IsBbutton(GetInputOB))
            {
                // ステージ名をGameManagerに保存
                GameManager.Instance.stageIndex = CurrentPage + 1; // stage001～stage015
                GameManager.Instance.SelectedStageName = $"stage{GameManager.Instance.stageIndex:D3}";

                SceneManager.LoadScene(2); // ステージ読み込み先シーン名
            }
        }
        else
        {
            //ページ移動のクールタイム
            PageCoolTimeCount += PageCoolTime / 10; //そのままだと数字が大きすぎるから調整
        }

        //矢印の表示を管理（例：これ以上左に行けないときに非表示
        if (CurrentPage == 0) LeftArrow.enabled = false;
        else if (CurrentPage == MaxPage) RightArrow.enabled = false;
        else
        {
            LeftArrow.enabled = true;
            RightArrow.enabled = true;
        }

    }

    //プログラム整理中につき一旦コメントアウト
    //public void SlideLeft()
    //{
    //    if (CurrentPage > 0)
    //    {
    //        CurrentPage--;
    //        TargetPosition = new Vector2(-pageWidth * CurrentPage, 0);
    //    }
    //}
    //public void SlideRight()
    //{
    //    if (CurrentPage < MaxPage)
    //    {
    //        CurrentPage++;
    //        TargetPosition = new Vector2(-pageWidth * CurrentPage, 0);
    //    }
    //}
}
