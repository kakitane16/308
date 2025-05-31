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
    public float maxDistance = 5f; // �X�R�A�[���ɂȂ鋗��
    public float levelMultiplier = 1f;

    private void Start()
    {
        // goal�i�^�[�Q�b�gTransform�j�����ݒ�Ȃ�T��
        if (goal == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(goalTag);
            if (player != null)
            {
                goal = player.transform;
            }
            else
            {
                Debug.LogWarning("�I�u�W�F�N�g��������܂���ł���");
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

        // �X�R�A�ۑ�
        if (GameManager.Instance != null)
        {
            GameManager.Instance.score = rank;
        }
    }

    //player��goal�̒��S�_�̋����œ_���i0~100�_�j
    private int CalculateScore(float distance)
    {
        float adjustedDistance = distance * levelMultiplier;
        float normalized = Mathf.Clamp01(1f - (adjustedDistance / maxDistance));
        return Mathf.RoundToInt(normalized * maxScore);
    }

    //CaluculateScore�ŊT�Z�����l�𐳎��ȃX�R�A�ɕϊ�
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
