using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UI_S_Move : MonoBehaviour
{
    public RectTransform content;       // ������UI�iPanel_Content�j
    public float pageWidth = 800f;      // 1�y�[�W�̕�
    public float slideSpeed = 10f;      // �ړ����x
    private Vector2 targetPosition;     // �ړ���̈ʒu
    private int currentPage = 0;        // ���݂̃y�[�W

    public int maxPage = 2; // �y�[�W�� - 1�i��F3�y�[�W�Ȃ�2�j

    public float PageCoolTime;

    private GamePadCommand _command;
    private int GetInputOB;
    private int count;

    void Start()
    {
        targetPosition = content.anchoredPosition;

        _command = new GamePadCommand();
        GetInputOB = (int)GameManager.Instance.inputDevice;
    }

    // Update is called once per frame
    void Update()
    {
        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, Time.deltaTime * slideSpeed);

        if (PageCoolTime >= 2.0f)
        {
            if (_command.LeftAction(GetInputOB))
            {
                if (currentPage > 0)
                {
                    currentPage--;
                    targetPosition = new Vector2(-pageWidth * currentPage, 0);
                    PageCoolTime = 0.0f;
                }
            }
            if (_command.RightAction(GetInputOB))
            {
                if (currentPage < maxPage)
                {
                    currentPage++;
                    targetPosition = new Vector2(-pageWidth * currentPage, 0);
                    PageCoolTime = 0.0f;
                }
            }

            if (_command.IsBbutton(GetInputOB))
            {
                switch (currentPage)
                {
                    case 0:
                        SceneManager.LoadScene("Title");
                        break;
                    case 1:
                        SceneManager.LoadScene("Game");
                        break;
                }
            }
        }
        else
        {
            PageCoolTime += 0.5f;
        }

    }
    public void SlideLeft()
    {
        if (currentPage > 0)
        {
            currentPage--;
            targetPosition = new Vector2(-pageWidth * currentPage, 0);
        }
    }
    public void SlideRight()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPosition = new Vector2(-pageWidth * currentPage, 0);
        }
    }
}
