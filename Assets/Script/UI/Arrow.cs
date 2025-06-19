using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject player;
    public GameObject ArrowUI;
    public string ArrowTag = "Player"; // �^�[�Q�b�g�̃^�O

    public Transform T_player;

    void Start()    // ����̊J�����ł̓���m�F�p�̉��u���ł��A�v���n�u�����łɊJ�����؂�ւ�����i�K�ō폜���Ă�������
    {
        if (T_player == null) return;

        //***UI�̏������W���v���C���[�i�l�^���j�̂悱�ɒu��***
        //�@�@�@�l�^�͖��񓯂��ꏊ����n�܂�Ȃ�����
        //player�̍��W���擾
        Vector3 playerPos = T_player.position + new Vector3(-7, 8, 0);
        //player�̍��W�Ɣ�邽�ߏ������炷
        ArrowUI.transform.position = playerPos;
    }
    // Start is called before the first frame update
    void LateUpdate()
    {
        if (T_player != null) return;

        // ***�v���C���[��Transform���擾***
        GameObject player = GameObject.FindGameObjectWithTag(ArrowTag);
        if (player != null)
        {
            player = player.transform.gameObject;
            T_player = player.transform;
        }

        if (T_player == null) return;

        //***UI�̏������W���v���C���[�i�l�^���j�̂悱�ɒu��***
        //�@�@�@�l�^�͖��񓯂��ꏊ����n�܂�Ȃ�����
        //player�̍��W���擾
        Vector3 playerPos = T_player.position + new Vector3(-7,8,0);
        //player�̍��W�Ɣ�邽�ߏ������炷
        ArrowUI.transform.position = playerPos;
        // (������ɏ������ړ����܂����AT_player��null�ł͐���Ɏ擾�ł��Ȃ�����)
    }
}
