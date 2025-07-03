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
    public float maxDistance = 5f; // �X�R�A�[���ɂȂ鋗��
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
                Debug.LogError("Goal.cs: OnCollisionEnter ���ɂ� goal ��������܂���ł����I");
                return;
            }
        }

        float distance = Vector3.Distance(transform.position, goal.position);
        int score = CalculateScore(distance);

        //Gimmick�ɂ��X�R�A�̍X�V����
        score = GimmickScore(score);

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

    private int GimmickScore(int score)
    {
        //�X�e�[�W�ŕ]������ς�邽��
        //�Q�[���Z���N�g����CSV�ǂݍ��ݒl�̓Q�[���}�l�[�W���[�ɕۑ�
        //���̒l�������Ă���
         int GimmickRank = GameManager.Instance.Points;

        switch (GimmickRank)
        {
            //�ʏ�̃m�������Ȃ̂�score�ϓ��Ȃ�
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

    //CaluculateScore�ŊT�Z�����l�𐳎��ȃX�R�A�ɕϊ�
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
