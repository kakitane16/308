using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Player py;
    public GameObject player;
    public GameObject ArrowUI;
    // Start is called before the first frame update
    void Start()
    {
        //***UI�̏������W���v���C���[�i�l�^���j�̂悱�ɒu��***
        //�@�@�@�l�^�͖��񓯂��ꏊ����n�܂�Ȃ�����
        //player�̍��W���擾
        //Vector2 playerPos = new Vector2(player.transform.position.x - 20.0f, player.transform.position.y - 10.0f);
        Vector2 playerPos = new Vector2(player.transform.position.x - 20.0f, player.transform.position.y);
        //player�̍��W�Ɣ�邽�ߏ������炷
        ArrowUI.transform.position = playerPos;
    }

    // Update is called once per frame
    void Update()
    {
        //float rotateAgl = py.SAngleY + 10.0f;
        //ArrowUI.transform.Rotate(0, rotateAgl, 0);
        //ArrowRotate();
    }

    //Arrow�̌������v���C���[������̒l�����ɕϊ�
    private void ArrowRotate()
    {
        
    }
}
