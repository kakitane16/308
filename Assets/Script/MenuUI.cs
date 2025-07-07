using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    private RectTransform imageRect1;
    private RectTransform imageRect2;
    // Start is called before the first frame update
    void Start()
    {
        imageRect1 = transform.Find("M_BackGroundUI/M_CloseUI").GetComponent<RectTransform>();
        imageRect2 = transform.Find("M_BackGroundUI/M_QuitUI").GetComponent<RectTransform>();
        // UI初期化
        imageRect1.anchoredPosition = new Vector2(0, 100);
        imageRect1.sizeDelta = new Vector2(500, 150);
        imageRect2.anchoredPosition = new Vector2(0, -80);
        imageRect2.sizeDelta = new Vector2(400, 120);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 表示関数
    public void ShowPanel()
    {
        GameObject obj = GameObject.Find("Menu");

        Transform panel = obj.transform.Find("M_BackGroundUI");

        panel.gameObject.SetActive(true);
    }

    // 非表示関数
    public void HidePanel()
    {
        GameObject obj = GameObject.Find("Menu");

        Transform panel = obj.transform.Find("M_BackGroundUI");

        panel.gameObject.SetActive(false);
    }
    // UI強調表示
   public void SelectUI(int select)
    {
        switch (select)
        {
            case 0:
                // ContineuImageの位置とサイズ変更
                // 位置を変更
                imageRect1.anchoredPosition = new Vector2(0, 100);
                // サイズを変更
                imageRect1.sizeDelta = new Vector2(500, 150);
                //　QuitImageの位置とサイズを変更
                imageRect2.anchoredPosition = new Vector2(0, -80);
                imageRect2.sizeDelta = new Vector2(400, 120);
                break;
            case 1:
                imageRect2.anchoredPosition = new Vector2(0, -100);
                imageRect2.sizeDelta = new Vector2(500, 150);

                imageRect1.anchoredPosition = new Vector2(0, 80);
                imageRect1.sizeDelta = new Vector2(400, 120);
                break;
        }
    }
}
