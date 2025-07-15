using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Tutorial_Con : MonoBehaviour
{
    public int ImageNum;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            ShowPanel(1);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            ShowPanel(2);

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowPanel(3);

        }
    }

    // 表示関数
    public void ShowPanel(int setImageNum)
    {
        ImageNum = setImageNum;
        GameObject obj = GameObject.Find("OperationUI");

        Transform panel = obj.transform.Find("OP_panel");
        Image img = obj.transform.Find("OP_panel").GetComponent<Image>();
        panel.gameObject.SetActive(true);

        // ギミックの画像素材はAssets/Resource/OperationUIフォルダ内に"Image_番号"のように保存する
        // ファイル名を組み立て(例：Image_1)
        string path = $"OperationUI/Image_{setImageNum}";

        // ResourceからSpriteをロード
        Sprite newSprite = Resources.Load<Sprite>(path);
        if (newSprite != null)
        {
            img.sprite = newSprite;
        }
        else
        {
            Debug.Log("読み込み失敗");
        }

    }
    // 非表示関数
    public void HidePanel()
    {
        GameObject obj = GameObject.Find("OperationUI");

        Transform panel = obj.transform.Find("OP_panel");

        panel.gameObject.SetActive(false);
    }
}
//------------関数の呼び出し-------------//
//private UI_Tutorial UISetter;
//
// Start内
//  GameObject TutorialUI = GameObject.Find("Tutorial");
//
// Update内
// if(呼び出す任意の条件)
// {
//     UISetter.ShowPanel();
// }
//