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

    private void Start()
    {
        _command = new GamePadCommand();
        GetInputOB = (int)GameManager.Instance.inputDevice;
        count = 0;
    }
    private void Update()
    {
        if(_command.LeftAction(GetInputOB))
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
                    SceneManager.LoadScene("Title");
                    break;
                case 1:
                    SceneManager.LoadScene("Game");
                    break;
            }
        }
    }

    //クリックするとゲームシーンへ移動
    //public void ClickButtonChangeGame()
    //{
    //    SceneManager.LoadScene("Game");
    //}

    ////クリックするとタイトルシーンへ移動（現状の仮で）
    //public void ClickButtonChangeTitle()
    //{
    //    SceneManager.LoadScene("Title");
    //}
}

