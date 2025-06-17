using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̃X�N���v�g�ł̓v���C���[�̒l������s�����ߊ�{�I�ɂ�Update�͎g��Ȃ��ōs��������
public class Sasa : MonoBehaviour
{
    private Player player;
    private float ShotPower;

    //player�I�u�W�F�N�g���ǂꂭ�炢������������
    //������������v���C���[���ɓn�������Ȃ̂�public(�g��Ȃ��Ȃ�private�ɐ؂�ւ��Ƃ���
    public float MoveX; 
    public float MoveZ;

    private void Start()
    {
        player = GetComponent<Player>();

        //����X�V�������邪��쓮���Ȃ��悤�ɏ������������Ēu��
        ShotPower = 0.0f; 
        MoveX = 0.0f;
        MoveZ = 0.0f;
    }

    private void OnCollisionEnter(Collision p_collision)
    {
        ChangePos();
    }

    public void ChangePos()
    {
        //�v���C���[��forceStrength�͒l�I�ɍő�l20f�Ŏ����Ă��Ă��邽��
        //�}�b�v�����܂ł̍��W�Ƃ������Ƃ�20f��2.5f��������΂Ȃ�
        //�������A�ŏ��l��0.0f or�@2.5f�ɂȂ邽�߂����肵�����ꍇ�͍Đ݌v���K�v
        ShotPower = player.forceStrength * 2.5f;

        //�ǂꂮ�炢�ɒl��������
        MoveX = ShotPower;

        //�v���C���[�̍��W���M�~�b�N���ōX�V��������
        //�ŏ��͍ő�l�̍��W�ɂ������Ƃ���
        player.rb.position = new Vector3(MoveX, 0.0f, MoveZ);
        
        //���̋����ł͊�{�I�ɂ͏d�͂��Ȃ����ߏ���
        player.rb.useGravity = false;
    }
}
