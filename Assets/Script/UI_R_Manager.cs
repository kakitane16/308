using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.UIElements;

//===============================================
//���U���g��UI�\�����}�l�W�����g����v���O����
//===============================================


//-------------------------------------------------------------------
//�X�y�[�X�L�[����������UI�\���i���͋q���x�肾�����̉��o�C�x���g�����s�̑���j
//�]����̒l��ϐ�Num�i���j�ɓ����
//���̃X�e�[�W���ǂ��Ȃ̂��̒l���~�����B�ϐ�StageNumber�ɓ����
//�]�����Ƃ̉��o�͑f�ނ��Ȃ����ߍ��͓��ɂȂ�
//5��9��  �H�R�y��
//-------------------------------------------------------------------


//�]���w��
public enum review
{
    Bad = 0,
    Nomal,
    Good,
    Perfect 
}


public class UI_R_Manager : MonoBehaviour
{

    public int Num;             //�ϐ��͉��B�]���ɂ���ē���鐔����ς���i�}�[�x���X�Ȃ�0,�����Ȃ�1�Ȃǁj
    public int StageNumber;     //�X�e�[�W�̔ԍ�������ϐ�

    public GameObject stage_object = null;  //Text�I�u�W�F�N�g

    public Canvas canvas;   //�L�����o�X


    void Start()
    {
        //�ŏ��͕\�����Ȃ��悤�ɃL�����o�X�̕\�����I�t
        canvas.enabled = false;
    }

    void Update()
    {
        //�I�u�W�F�N�g����Text�R���|�[�l���g���擾
        Text stage_text = stage_object.GetComponent<Text>();

        //�X�e�[�W���̕\��
        if (StageNumber == 0) { stage_text.text = "000000"/*���̃X�e�[�W��*/; }
        if (StageNumber == 1) { stage_text.text = "111111"/*���̃X�e�[�W��*/; }



        //�X�y�[�X�L�[����������UI�\���i���͋q���x�肾�����̉��o�C�x���g�����s�̑���j
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            canvas.enabled = !canvas.enabled;
        }

        //�]���ɂ���ĉ��o��ς���
        switch (Num)
        {
            case (int)review.Bad:
                break;

            case (int)review.Nomal:
                break;

            case (int)review.Good:
                break;

            case (int)review.Perfect:
                break;
        }

    }
   
    public void GetNum(int Point)
    {
        Num = Point;
    }
}
