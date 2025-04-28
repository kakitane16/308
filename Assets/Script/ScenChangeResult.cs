using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScenChangeResult : MonoBehaviour
{
    //クリックするとゲームシーンへ移動
    public void ClickButtonChangeSceneGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
