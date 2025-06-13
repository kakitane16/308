using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UI_S_Move : MonoBehaviour
{
    public RectTransform content;       // 動かすUI（Panel_Content）
    public float pageWidth = 800f;      // 1ページの幅
    public float slideSpeed = 10f;      // 移動速度
    private Vector2 targetPosition;     // 移動先の位置
    private int currentPage = 0;        // 現在のページ

    public int maxPage = 2; // ページ数 - 1（例：3ページなら2）

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
