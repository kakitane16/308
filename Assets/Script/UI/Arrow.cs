using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject player;
    public GameObject ArrowUI;

    public Transform T_player;
    // Start is called before the first frame update
    void Start()
    {
        //***UI�̏������W���v���C���[�i�l�^���j�̂悱�ɒu��***
        //�@�@�@�l�^�͖��񓯂��ꏊ����n�܂�Ȃ�����
        //player�̍��W���擾
        Vector3 playerPos = T_player.position + new Vector3(-8,7,0);
        //player�̍��W�Ɣ�邽�ߏ������炷
        ArrowUI.transform.position = playerPos;
    }
}
