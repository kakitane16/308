using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class BackTitle : MonoBehaviour
{
    GamePadCommand command;
    int change;
    private bool isEsc = false;
    public void Start()
    {
        command = new GamePadCommand();
        change = 0;//���݂̈ʒu�@0����@�P����
        isEsc = false;
    }

    public void Update()
    {
        //�Q�[���p�b�h��L�[�{�[�h�œ����������ł���悤�ȃv���O�������擾
        command = new GamePadCommand();

        //ESC�L�[���������܂ł͈ȉ��̏����͓���Ȃ�
        if (command.GetEscKey((int)GameManager.Instance.inputDevice))
        {
            isEsc = true;
        }
    
        if(isEsc)
        {
            //���UI�ړ�
            if (command.UpAction((int)GameManager.Instance.inputDevice))
            {
                if (change == 1)
                {
                    change = 0;
                }
            }
            //���Ɉړ�
            if (command.DownAction((int)GameManager.Instance.inputDevice))
            {
                if (change == 0)
                {
                    change = 1;
                }
            }

            //�������������������������������̏���������������������������
            if (command.IsBbutton((int)GameManager.Instance.inputDevice))
            {
                //���ꂼ���UI�̈ʒu�쏈��
                switch (change)
                {
                    //��̏ꍇ
                    case 0:
                        SceneManager.LoadScene("Title");
                        break;
                    //���̏ꍇ
                    case 1:
                        break;
                }
            }
        }

        
    }
}
