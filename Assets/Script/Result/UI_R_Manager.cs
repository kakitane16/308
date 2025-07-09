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
    public Canvas Good;
    public Canvas Perfect;


    void Start()
    {
        OneCount = true;
        //最初は表示しないようにキャンバスの表示をオフ
        Bad.enabled = false;
        Nomal.enabled = false;
        Good.enabled = false;
        Perfect.enabled = false;
    }

    void Update()
    {
        //＊＊＊ゲームシーンのスコアを取得＊＊＊＊
        Num = GameManager.Instance.score;

        //オブジェクトからTextコンポーネントを取得
        Text stage_text = stage_object.GetComponent<Text>();

        if (OneCount)
        {
            //評価によって演出を変える
            switch (Num)
            {
                case (int)review.Bad:
                    Bad.enabled = !Bad.enabled;
                    break;

                case (int)review.Nomal:
                    Nomal.enabled = !Nomal.enabled;
                    break;

                case (int)review.Perfect:
                    Perfect.enabled = !Perfect.enabled;
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
