using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScenChangeResult : MonoBehaviour
{
    private GamePadCommand _command;
    private int GetInputOB;
    private int count;

    public int Num;             //変数は仮。評価によって入れる数字を変える（マーベラスなら0,完璧なら1など）


    private void Start()
    {
        _command = new GamePadCommand();
        GetInputOB = (int)GameManager.Instance.inputDevice;
        count = 1;
    }
    private void Update()
    {
        //＊＊＊ゲームシーンのスコアを取得＊＊＊＊
        Num = GameManager.Instance.score;

        if (_command.LeftAction(GetInputOB))
        {
            count = 1;
        }
        if (_command.RightAction(GetInputOB))
        {
            count = 0;
        }

        if (_command.IsBbutton(GetInputOB))
        {
            switch (count)
            {
                case 0:
                    SceneManager.LoadScene("Select");
                    break;
                case 1:
                    if (Num == (int)review.Perfect)//パーフェクトのためステージを次に進める
                        GameManager.Instance.stageIndex = GameManager.Instance.stageIndex + 1;
                    GameManager.Instance.SelectedStageName = $"stage{GameManager.Instance.stageIndex:D3}";
                    SceneManager.LoadScene(2);
                    break;
            }
        }
    }
    public void GetNum(int Point)
    {
        Num = Point;
    }
}

