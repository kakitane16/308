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

    public int MaxStageIndex = 15;

    private bool isProcessing = false; //処理中フラグ

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

        if (_command.IsBbutton(GetInputOB) && !isProcessing)
        {
            isProcessing = true;
            switch (count)
            {
                case 0:
                    SceneManager.LoadScene("Select");
                    break;
                case 1:
                    //Normal以上のスコアの時ステージを次に進める
                    if (Num >= (int)review.Nomal)
                    {
                        int max = GameManager.Instance.MaxPage;
                        GameManager.Instance.stageIndex = Mathf.Min(GameManager.Instance.stageIndex + 1, max);
                        //
                        if (GameManager.Instance.clearstate < GameManager.Instance.stageIndex)
                        {
                            GameManager.Instance.clearstate = GameManager.Instance.stageIndex;
                        }

                        Debug.Log($"[進行] ステージを進行: {GameManager.Instance.stageIndex}, 評価: {Num}");
                    }
                    GameManager.Instance.SelectedStageName = $"stage{GameManager.Instance.stageIndex:D3}";
                    GameManager.Instance.SaveClearState();
                    GameManager.Instance.Fast = true;
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

