using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScenChangeResult : MonoBehaviour
{
    public Player pr;
    //クリックするとゲームシーンへ移動
    public void ClickButtonChangeGame()
    {
        SceneManager.LoadScene("Game");
        pr.ResetSceneFlag();
    }

    //クリックするとタイトルシーンへ移動（現状の仮で）
    public void ClickButtonChangeTitle()
    {
        SceneManager.LoadScene("Title");
    }
}

