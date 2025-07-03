using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class Goal : MonoBehaviour
{
    private Transform goal;
    private string goalTag = "Plate";
    public string syari = "Syari";
    public float maxScore = 100f;
    public float maxDistance = 5f; // スコアゼロになる距離
    public float levelMultiplier = 1f;

    void LateUpdate()
    {
        GameObject player = GameObject.FindGameObjectWithTag(goalTag);
        if (player != null) goal = player.transform;
    }

    private void OnCollisionEnter(Collision gl)
    {
        if (goal == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(goalTag);
            if (player != null)
            {
                goal = player.transform;
            }
            else
            {
                Debug.LogError("Goal.cs: OnCollisionEnter 時にも goal が見つかりませんでした！");
                return;
            }
        }

        float distance = Vector3.Distance(transform.position, goal.position);
        int score = CalculateScore(distance);

        //Gimmickによるスコアの更新処理
        score = GimmickScore(score);

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

    private int GimmickScore(int score)
    {
        //ステージで評価基準が変わるため
        //ゲームセレクトからCSV読み込み値はゲームマネージャーに保存
        //その値を持ってくる
         int GimmickRank = GameManager.Instance.Points;

        switch (GimmickRank)
        {
            //通常のノリだけなのでscore変動なし
            case 10:
                return score;

            case 11:
                break;

            case 12:
                break;

            case 13:
                break;
        }

        return score;
    }

    //CaluculateScoreで概算した値を正式なスコアに変換
    private int GetScoreRank(int score)
    {
        if (score <= 10)
        {
            Debug.Log("bad");
            return (int)review.Bad;
        }
        else if (score <= 60)
        {
            Debug.Log("normal");
            return (int)review.Nomal;
        }
        else if (score <= 80)
        {
            Debug.Log("good");
            return (int)review.Good;
        }
        else
        {
            Debug.Log("perfect");
            return (int)review.Perfect;
        }
    }

}
