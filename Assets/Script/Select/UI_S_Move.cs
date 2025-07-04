using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_S_Move : MonoBehaviour
{
    public RectTransform content;       // ������UI�iPanel_Content�j
    public int MaxPage;                 // �y�[�W�� - 1�i��F3�y�[�W�Ȃ�2�j
    public float PageWidth;             // 1�y�[�W�̕�
    public float SlideSpeed;            // �ړ����x
    public float PageCoolTime;          //�y�[�W�̃N�[���^�C��
    private Vector2 TargetPosition;     // �ړ���̈ʒu
    private int CurrentPage;            // ���݂̃y�[�W
    float PageCoolTimeCount;            //�N�[���^�C���J�E���g�p
    //�R���g���[���[
    private GamePadCommand _command;
    private int GetInputOB;
    //���̕\���Ǘ�
    public Image LeftArrow;
    public Image RightArrow;


    void Start()
    {
        //canvas��Pos
        TargetPosition = content.anchoredPosition;

        //Pad�̏�����
        _command = new GamePadCommand();
        GetInputOB = (int)GameManager.Instance.inputDevice;
    }


    void Update()
    {
        //canvas��Pos�ړ�
        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, TargetPosition, Time.deltaTime * SlideSpeed);

        //�R���g���[���[����
        if (PageCoolTimeCount >= 5.0f)
        {
            //�y�[�W�̈ړ�
            if (_command.LeftAction(GetInputOB))
            {
                if (CurrentPage > 0)
                {
                    CurrentPage--;
                    TargetPosition = new Vector2(-PageWidth * CurrentPage, 0);
                    PageCoolTimeCount = 0.0f;
                }
            }
            if (_command.RightAction(GetInputOB))
            {
                if (CurrentPage < MaxPage)
                {
                    CurrentPage++;
                    TargetPosition = new Vector2(-PageWidth * CurrentPage, 0);
                    PageCoolTimeCount = 0.0f;
                }
            }

            //����{�^������
            if (_command.IsBbutton(GetInputOB))
            {
                // �X�e�[�W����GameManager�ɕۑ�
                GameManager.Instance.stageIndex = CurrentPage + 1; // stage001�`stage015
                GameManager.Instance.SelectedStageName = $"stage{GameManager.Instance.stageIndex:D3}";

                SceneManager.LoadScene(2); // �X�e�[�W�ǂݍ��ݐ�V�[����
            }
        }
        else
        {
            //�y�[�W�ړ��̃N�[���^�C��
            PageCoolTimeCount += PageCoolTime / 10; //���̂܂܂��Ɛ������傫�����邩�璲��
        }

        //���̕\�����Ǘ��i��F����ȏ㍶�ɍs���Ȃ��Ƃ��ɔ�\��
        if (CurrentPage == 0) LeftArrow.enabled = false;
        else if (CurrentPage == MaxPage) RightArrow.enabled = false;
        else
        {
            LeftArrow.enabled = true;
            RightArrow.enabled = true;
        }

    }

    //�v���O�����������ɂ���U�R�����g�A�E�g
    //public void SlideLeft()
    //{
    //    if (CurrentPage > 0)
    //    {
    //        CurrentPage--;
    //        TargetPosition = new Vector2(-pageWidth * CurrentPage, 0);
    //    }
    //}
    //public void SlideRight()
    //{
    //    if (CurrentPage < MaxPage)
    //    {
    //        CurrentPage++;
    //        TargetPosition = new Vector2(-pageWidth * CurrentPage, 0);
    //    }
    //}
}
