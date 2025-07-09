using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tutorial_Describe : MonoBehaviour
{

    private Image targetImage; // UIのImage
    private UI_Tutorial tutorial;
    // Start is called before the first frame update
    void Start()
    {
        targetImage = GameObject.Find("T_GimmickDescribe").GetComponent<Image>();
        Transform grandParent = transform.parent?.parent;
        tutorial = grandParent.GetComponent<UI_Tutorial>();
        
        //***********************************************************************************
        //古瀬君へ
        //緊急対応だったためUIの裏に他UIが重なってるので後で消しといて！！！
        
        //サイズ
        targetImage.rectTransform.sizeDelta = new Vector2(1000f, 600f);
        //位置
        targetImage.rectTransform.anchoredPosition = new Vector2(0f, 0f);
        
        //***********************************************************************************
    }

    // Update is called once per frame
    void Update()
    {
        SetDescribeNumber(tutorial.gimmickNum);
    }
    public void SetDescribeNumber(int number)
    {
        // ギミックの画像素材はAssets/Resource/Gimmicks_Dフォルダ内に"GimmickDescribe_番号"のように保存する
        // ファイル名を組み立て(例：GimmickDescribe_1)
        string path = $"Gimmicks_D/GimmickDescribe_{number}";

        // ResourceからSpriteをロード
        Sprite newSprite = Resources.Load<Sprite>(path);

        if (newSprite != null)
        {
            targetImage.sprite = newSprite;
        }
        else
        {
            Debug.Log("読み込み失敗");
            Debug.LogWarning("指定された画像が見つかりません: " + path);
        }
    }
}
//------------関数の呼び出し-------------//
//private UI_Tutorial_Describe iconSetter_D;
//
// Start内
//  GameObject iconR = GameObject.Find("T_GimmickDescribe");
//
// Update内
// if(呼び出す任意の条件)
// {
//     iconSetter_D.SetDescribeNumber(1);
// }
//

