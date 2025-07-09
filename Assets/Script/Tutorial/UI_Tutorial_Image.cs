using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tutorial_Image : MonoBehaviour
{
    private Image targetImage; // UIのImage
    private UI_Tutorial tutorial;
    // Start is called before the first frame update
    void Start()
    {
        targetImage = GameObject.Find("T_GimmickImage").GetComponent<Image>();
        Transform grandParent = transform.parent?.parent;
        tutorial = grandParent.GetComponent<UI_Tutorial>();

    }

    // Update is called once per frame
    void Update()
    {

        SetImageNumber(tutorial.gimmickNum);
    }
    public void SetImageNumber(int number)
    {
        // ギミックの画像素材はAssets/Resource/Gimmicks_Iフォルダ内に"GimmickImage_番号"のように保存する
        // ファイル名を組み立て(例：GimmickImage_1)
        string path = $"Gimmicks_I/GimmickImage_{number}";

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
//private UI_Tutorial_Image iconSetter_I;
//
// Start内
//  GameObject iconL = GameObject.Find("T_GimmickImage");
//
// Update内
// if(呼び出す任意の条件)
// {
//     iconSetter_I.SetImageNumber(1);
// }
//