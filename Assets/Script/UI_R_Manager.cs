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
    bool OneCount;
    public int Num;             //�ϐ��͉��B�]���ɂ���ē���鐔����ς���i�}�[�x���X�Ȃ�0,�����Ȃ�1�Ȃǁj
    public int StageNumber;     //�X�e�[�W�̔ԍ�������ϐ�

    public GameObject stage_object = null;  //Text�I�u�W�F�N�g

    //�L�����o�X(���ꂼ��̕]��)
    public Canvas Bad;
    public Canvas Nomal;
    public Canvas Good;
    public Canvas Perfect;


    void Start()
    {
        OneCount = true;
        //�ŏ��͕\�����Ȃ��悤�ɃL�����o�X�̕\�����I�t
        Bad.enabled = false;
        Nomal.enabled = false;
        Good.enabled = false;
        Perfect.enabled = false;
    }

    void Update()
    {
        //�������Q�[���V�[���̃X�R�A���擾��������
        Num = GameManager.Instance.score;


        //�I�u�W�F�N�g����Text�R���|�[�l���g���擾
        Text stage_text = stage_object.GetComponent<Text>();

        //�X�e�[�W���̕\��
        //if (StageNumber == 0) { stage_text.text = "000000"/*���̃X�e�[�W��*/; }
        //if (StageNumber == 1) { stage_text.text = "�X�e�[�W�P"/*���̃X�e�[�W��*/; }

        //�X�y�[�X�L�[����������UI�\���i���͋q���x�肾�����̉��o�C�x���g�����s�̑���j
        //if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        //{
        //    canvas.enabled = !canvas.enabled;
        //}


        if (OneCount)
        {
            //�]���ɂ���ĉ��o��ς���
            switch (Num)
            {
                case (int)review.Bad:
                    Bad.enabled = !Bad.enabled;
                    break;

                case (int)review.Nomal:
                    Nomal.enabled = !Nomal.enabled;
                    break;

                case (int)review.Good:
                    Good.enabled = !Good.enabled;
                    break;

                case (int)review.Perfect:
                    Perfect.enabled = !Perfect.enabled;
                    break;
            }
            OneCount = false;
        }
    }

    public void GetNum(int Point)
    {
        Num = Point;
    }
}
