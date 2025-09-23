using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalImage : MonoBehaviour
{
    public Image RECRUIT;      //Š®¬}‚ÌUI
    public Image ITA;
    private int RecruitPoint;   //Š®¬}‚Ì’l‚ğ•ÛŠÇ

    // Šeí—Ş‚ÌƒXƒvƒ‰ƒCƒg‚ğInspector‚Åİ’è
    public Sprite normalSushi;
    public Sprite wasabiSushi;
    public Sprite aburiSushi;
    public Sprite aburiWasabiSushi;
    public Sprite Ita;

    public void UpdateImageFromPoints()
    {
        RecruitPoint = GameManager.Instance.Points;

        //”Â‚Ì•`‰æ
        ITA.rectTransform.anchoredPosition = new Vector2(600.0f, -330.0f);
        ITA.rectTransform.sizeDelta = new Vector2(550.0f, 400.0f);
        ITA.sprite = Ita;

        //õi‚Ì•`‰æ
        RECRUIT.rectTransform.anchoredPosition = new Vector2(600.0f, -350.0f);
        RECRUIT.rectTransform.sizeDelta = new Vector2(400.0f, 300.0f);

        //‚È‚ñ‚Ìõi‚ğ•\¦‚·‚é‚©
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
            default: Debug.LogWarning("–¢‘Î‰‚ÌRecruitPoint: " + RecruitPoint);
                break;
        }
    }
}
