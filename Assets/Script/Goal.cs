using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class Goal : MonoBehaviour
{
    public Transform goal;
    public float maxScore = 100f;
    public int score;
    public float Level;
    private UI_R_Manager ui_manager;

    private void Start()
    {
        ui_manager = FindObjectOfType<UI_R_Manager>();
    }

    private void OnCollisionEnter(Collision gl)
    {
        if (gl.gameObject.CompareTag("Player"))
        {
            //シャリとネタの中心点の距離を捕る
            float distance = Vector3.Distance(transform.position, goal.position);
            //スコアが0以上100以下に設定
            //値が大きいのでレベルの値を大きくすると引かれる値が大きくなる
            score = (int)Mathf.Clamp((int)(maxScore - (distance * Level)), 0, maxScore);

        }

        //判定基準　bad <= 20  Nomal <= 40 Good <= 60  81 <= Perfect <= 100 
        if (score <= 10)
        {
            //ui_manager.GetNum((int)review.Bad);
            SceneManager.LoadScene(2);
            Debug.Log("bad");
        }
        else if (score <= 60)
        {
            //ui_manager.GetNum((int)review.Nomal);
            Debug.Log("nomal");
        }
        else if (score <= 80)
        {
           // ui_manager.GetNum((int)review.Good);
            Debug.Log("good");
        }
        else if (score <= 100)
        {
           // ui_manager.GetNum((int)review.Perfect);
            Debug.Log("parfect");
        }

       // SceneManager.LoadScene(2);
    }
}
