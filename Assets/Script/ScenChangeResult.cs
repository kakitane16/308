using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScenChangeResult : MonoBehaviour
{
    //�N���b�N����ƃQ�[���V�[���ֈړ�
    public void ClickButtonChangeSceneGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
