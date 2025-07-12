using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalImage : MonoBehaviour
{
    public Image RECRUIT;      //完成図のUI
    private int RecruitPoint;   //完成図の値を保管

    // 各種類のスプライトをInspectorで設定
    public Sprite normalSushi;
    public Sprite wasabiSushi;
    public Sprite aburiSushi;
    public Sprite aburiWasabiSushi;

    public void UpdateImageFromPoints()
    {
        RecruitPoint = GameManager.Instance.Points;

        RECRUIT.rectTransform.anchoredPosition = new Vector2(600.0f, -350.0f);
        RECRUIT.rectTransform.sizeDelta = new Vector2(500.0f, 400.0f);

        //なんの寿司を表示するか
        switch (RecruitPoint)
        {
            case 10: RECRUIT.sprite = normalSushi; 
                break;
            case 11: RECRUIT.sprite = wasabiSushi; 
                break;
            case 12: RECRUIT.sprite = aburiSushi;
                break;
            case 13: RECRUIT.sprite = aburiWasabiSushi; 
                break;
            default: Debug.LogWarning("未対応のRecruitPoint: " + RecruitPoint);
                break;
        }
    }
}
