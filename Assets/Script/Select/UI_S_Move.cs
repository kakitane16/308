using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class UI_S_Move : MonoBehaviour
{
    public RectTransform content;       // 動かすUI（Panel_Content）
    public float pageWidth = 800f;      // 1ページの幅
    public float slideSpeed = 10f;      // 移動速度
    private Vector2 targetPosition;     // 移動先の位置
    private int currentPage = 0;        // 現在のページ

    public int maxPage = 2; // ページ数 - 1（例：3ページなら2）

    void Start()
    {
        targetPosition = content.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, Time.deltaTime * slideSpeed);
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
