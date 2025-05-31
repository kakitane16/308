using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject player;
    public GameObject ArrowUI;
    public string ArrowTag = "Player"; // �^�[�Q�b�g�̃^�O

    public Transform T_player;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(ArrowTag);
            if (player != null)
            {
                player = player.transform.gameObject;
                T_player = player.transform;
            }
            else
            {
                Debug.LogWarning("�I�u�W�F�N�g��������܂���ł���");
            }
        }

        //***UI�̏������W���v���C���[�i�l�^���j�̂悱�ɒu��***
        //�@�@�@�l�^�͖��񓯂��ꏊ����n�܂�Ȃ�����
        //player�̍��W���擾
        Vector3 playerPos = T_player.position + new Vector3(-7,8,0);
        //player�̍��W�Ɣ�邽�ߏ������炷
        ArrowUI.transform.position = playerPos;
    }
}
