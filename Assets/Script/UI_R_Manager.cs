using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.UIElements;

//===============================================
//リザルトのUI表示をマネジメントするプログラム
//===============================================


//-------------------------------------------------------------------
//スペースキーを押したらUI表示（今は客が踊りだす等の演出イベントが実行の代わり）
//評価基準の値を変数Num（仮）に入れる
//今のステージがどこなのかの値が欲しい。変数StageNumberに入れる
//評価ごとの演出は素材がないため今は特になし
//5月9日  秋山遥音
//-------------------------------------------------------------------


//評価指数
public enum review
{
    Bad = 0,
    Nomal,
    Good,
    Perfect 
}


public class UI_R_Manager : MonoBehaviour
{

    public int Num;             //変数は仮。評価によって入れる数字を変える（マーベラスなら0,完璧なら1など）
    public int StageNumber;     //ステージの番号を入れる変数

    public GameObject stage_object = null;  //Textオブジェクト

    public Canvas canvas;   //キャンバス


    void Start()
    {
        //最初は表示しないようにキャンバスの表示をオフ
        canvas.enabled = false;
    }

    void Update()
    {
        //オブジェクトからTextコンポーネントを取得
        Text stage_text = stage_object.GetComponent<Text>();

        //ステージ名の表示
        if (StageNumber == 0) { stage_text.text = "000000"/*仮のステージ名*/; }
        if (StageNumber == 1) { stage_text.text = "111111"/*仮のステージ名*/; }



        //スペースキーを押したらUI表示（今は客が踊りだす等の演出イベントが実行の代わり）
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            canvas.enabled = !canvas.enabled;
        }

        //評価によって演出を変える
        switch (Num)
        {
            case (int)review.Bad:
                break;

            case (int)review.Nomal:
                break;

            case (int)review.Good:
                break;

            case (int)review.Perfect:
                break;
        }

    }
   
    public void GetNum(int Point)
    {
        Num = Point;
    }
}
