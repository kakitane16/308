using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class Goal : MonoBehaviour
{
    public Transform goal;
    public float maxScore = 100f;
    private int score;
    public float Level;

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision gl)
    {
        if (gl.gameObject.CompareTag("syari"))
        {
            //シャリとネタの中心点の距離を捕る
            float distance = Vector3.Distance(transform.position, goal.position);
            //スコアが0以上100以下に設定
            //値が大きいのでレベルの値を大きくすると引かれる値が大きくなる
            score = (int)Mathf.Clamp(maxScore - (distance * Level), 0f, maxScore);

            Debug.Log("ゴールに触れた！ スコア: " + score);
        }

        //判定基準　bad <= 20  Nomal <= 40 Good <= 60  Great <= 80   81 <= Perfect <= 100 
        if(score <= 10)
        {
            Debug.Log("全然ダメ");
        }
        else if(score <= 70)
        {
            Debug.Log("平凡");
        }
        else if(score <= 100)
        {
            Debug.Log("完璧");
        }
        else if(score <= 100)
        {
            Debug.Log("マーベラス");
        }
    }
}
