using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class Goal : MonoBehaviour
{
    public Transform goal;
    public string goalTag = "Plate";
    public float maxScore = 100f;
    public float maxDistance = 5f; // スコアゼロになる距離
    public float levelMultiplier = 1f;

    private void Start()
    {
        // goal（ターゲットTransform）が未設定なら探す
        if (goal == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(goalTag);
            if (player != null)
            {
                goal = player.transform;
            }
            else
            {
                Debug.LogWarning("オブジェクトが見つかりませんでした");
            }
        }
    }

    private void OnCollisionEnter(Collision gl)
    {
        if (!gl.gameObject.CompareTag(goalTag))
        {
            return;
        }


        float distance = Vector3.Distance(transform.position, goal.position);
        int score = CalculateScore(distance);
        int rank = GetScoreRank(score);

        // スコア保存
        if (GameManager.Instance != null)
        {
            GameManager.Instance.score = rank;
        }
    }

    //playerとgoalの中心点の距離で点数（0~100点）
    private int CalculateScore(float distance)
    {
        float adjustedDistance = distance * levelMultiplier;
        float normalized = Mathf.Clamp01(1f - (adjustedDistance / maxDistance));
        return Mathf.RoundToInt(normalized * maxScore);
    }

    //CaluculateScoreで概算した値を正式なスコアに変換
    private int GetScoreRank(int score)
    {
        if (score <= 10)
        {
            Debug.Log("bad");
            return 0;
        }
        else if (score <= 60)
        {
            Debug.Log("normal");
            return 1;
        }
        else if (score <= 80)
        {
            Debug.Log("good");
            return 2;
        }
        else
        {
            Debug.Log("perfect");
            return 3;
        }
    }

}
