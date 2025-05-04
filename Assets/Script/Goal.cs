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
            //�V�����ƃl�^�̒��S�_�̋�����߂�
            float distance = Vector3.Distance(transform.position, goal.position);
            //�X�R�A��0�ȏ�100�ȉ��ɐݒ�
            //�l���傫���̂Ń��x���̒l��傫������ƈ������l���傫���Ȃ�
            score = (int)Mathf.Clamp(maxScore - (distance * Level), 0f, maxScore);

            Debug.Log("�S�[���ɐG�ꂽ�I �X�R�A: " + score);
        }

        //�����@bad <= 20  Nomal <= 40 Good <= 60  Great <= 80   81 <= Perfect <= 100 
        if(score <= 20)
        {
            Debug.Log("Bad");
        }
        else if(score <= 40)
        {
            Debug.Log("Nomal");
        }
        else if(score <= 60)
        {
            Debug.Log("Good");
        }
        else if(score <= 80)
        {
            Debug.Log("Great");
        }
        else
        {
            Debug.Log("Perfect");
        }
    }
}
