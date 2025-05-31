using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitlePadChange : MonoBehaviour
{
    private GamePadCommand _command;
    private int GetInputOB;
    private int count;
    private const int optionCount = 3; // Game, Manual, Quit
    private void Start()
    {
        _command = new GamePadCommand();
        GetInputOB = (int)GameManager.Instance.inputDevice;
        count = 0;
    }
    private void Update()
    {
        if (_command.UpAction(GetInputOB))
        {
            count = (count - 1 + optionCount) % optionCount;
        }
        if (_command.DownAction(GetInputOB))
        {
            count = (count + 1) % optionCount;
        }

        if (_command.IsBbutton(GetInputOB))
        {
            switch (count)
            {
                case 0:
                    SceneManager.LoadScene("Game");
                    break;
                case 1:
                    SceneManager.LoadScene("Manual");
                    break;
                case 2:
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                    break;
            }
        }
    }

    //�N���b�N����ƃQ�[���V�[���ֈړ�
    //public void ClickButtonChangeGame()
    //{
    //    SceneManager.LoadScene("Game");
    //}

    ////�N���b�N����ƃ^�C�g���V�[���ֈړ��i����̉��Łj
    //public void ClickButtonChangeTitle()
    //{
    //    SceneManager.LoadScene("Title");
    //}
}

