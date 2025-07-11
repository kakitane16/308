using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.UIElements;

//===============================================
//リザルトのUI表示をマネジメントするプログラム
//===============================================

//評価指数
public enum review
{
    Bad = 0,
    Nomal,
    Perfect 
}


public class UI_R_Manager : MonoBehaviour
{
    bool OneCount;
    public int Num;             //変数は仮。評価によって入れる数字を変える（マーベラスなら0,完璧なら1など）
    public int StageNumber;     //ステージの番号を入れる変数

    public GameObject stage_object = null;  //Textオブジェクト

    //キャンバス(それぞれの評価)
    public Canvas Bad;
    public Canvas Nomal;
    public Canvas Perfect;


    void Start()
    {
        OneCount = true;
        //最初は表示しないようにキャンバスの表示をオフ
        Bad.enabled = false;
        Nomal.enabled = false;
        Perfect.enabled = false;
    }

    void Update()
    {
        //＊＊＊ゲームシーンのスコアを取得＊＊＊＊
        Num = GameManager.Instance.score;

        if (OneCount)
        {

            int currentStage = GameManager.Instance.stageIndex;

            if (Num >= (int)review.Nomal)//評価がNomal以上でクリア判定
            {
                if (GameManager.Instance.clearstate < currentStage)
                {
                    GameManager.Instance.clearstate = currentStage;
                    GameManager.Instance.SaveClearState(); // 保存
                }
            }

            // 一旦全キャンバスを非表示
            Bad.enabled = false;
            Nomal.enabled = false;
            Perfect.enabled = false;

            //評価によって演出を変える
            switch (Num)
            {
                case (int)review.Bad:
                    Bad.enabled = true;
                    break;

                case (int)review.Nomal:
                    Nomal.enabled = true;
                    break;

                case (int)review.Perfect:
                    Perfect.enabled = true;
                    break;
            }
            OneCount = false;
        }
    }

    public void GetNum(int Point)
    {
        Num = Point;
    }
}
