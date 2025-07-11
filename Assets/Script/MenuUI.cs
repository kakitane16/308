using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUI : MonoBehaviour
{
    private RectTransform imageRect1;
    private RectTransform imageRect2;
    public TextMeshProUGUI imageRect3;
    public TextMeshProUGUI imageRect4;
    private Vector2 imagePos;
    private Vector2 imageScale;
    private Vector2 imageScaleOff;
    public float animScale = 0.3f;   // 拡縮の大きさ
    public float animSpeed = 0.01f;   // 波の速さ
    private float timeCnt;
    int debugSelect;
    // Start is called before the first frame update
    void Start()
    {
        imageRect1 = transform.Find("M_BackGroundUI/M_CloseUI").GetComponent<RectTransform>();
        imageRect2 = transform.Find("M_BackGroundUI/M_QuitUI").GetComponent<RectTransform>();
        imageRect3 = transform.Find("M_BackGroundUI/M_QuitUI/QuitTex").GetComponent<TextMeshProUGUI>();
        imageRect4 = transform.Find("M_BackGroundUI/M_CloseUI/CloseTex").GetComponent<TextMeshProUGUI>();
        // UI初期化
        imageRect1.anchoredPosition = new Vector2(0, 100);
        imageRect1.sizeDelta = new Vector2(1000, 150);   

        imageRect2.anchoredPosition = new Vector2(0, -80);
        imageRect2.sizeDelta = new Vector2(840, 140);
   
        imagePos = new Vector2( 0, 100 );
        imageScale = new Vector2( 1000, 150 );
        imageScaleOff = new Vector2( 840, 140 );
        timeCnt = 0;
        SetAllTextColor(imageRect3, new Color32(0, 0, 0, 255));     
        SetAllTextColor(imageRect4, new Color32(255, 255, 255, 255)); 
        debugSelect = 0;
        HidePanel();
    }

    // Update is called once per frame
    void Update()
    {
        imageRect3.ForceMeshUpdate();
        imageRect4.ForceMeshUpdate();
        if (Input.GetKeyDown(KeyCode.Escape))
            ShowPanel();

        if (Input.GetKeyDown(KeyCode.M))
            debugSelect = 0;
        
        if (Input.GetKeyDown(KeyCode.N))
            debugSelect = 1;

        SelectUI(debugSelect);

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
                AnimUI(imageRect1);
                SetAllTextColor(imageRect4,new Color32(0,0,0,255));
                SetAllTextColor(imageRect3, new Color32(255, 255, 255, 255));
                AnimTex(imageRect4);
                //imageRect1.sizeDelta = new Vector2(500, 150);
                //　QuitImageの位置とサイズを変更
                imageRect2.anchoredPosition = new Vector2(0, -80);
               // imageRect4.anchoredPosition = new Vector2(0, -80);
                //imageRect2.sizeDelta = new Vector2(400, 120);
                imageRect2.sizeDelta = imageScaleOff;
                break;
            case 1:
                imageRect2.anchoredPosition = new Vector2(0, -100);
                //imageRect2.sizeDelta = new Vector2(500, 150);
                AnimUI(imageRect2);
                SetAllTextColor(imageRect3, new Color32(0, 0, 0, 255));
                SetAllTextColor(imageRect4, new Color32(255,255,255, 255));
                AnimTex(imageRect3);
                imageRect1.anchoredPosition = new Vector2(0, 80);
               // imageRect3.anchoredPosition = new Vector2(0, 80);
                //imageRect1.sizeDelta = new Vector2(400, 120);
                imageRect1.sizeDelta = imageScaleOff;
                break;
        }
    }
    void AnimUI(RectTransform imageRect)
    {
        // 時間経過に応じたsin波を計算
        float scale = 1.0f + Mathf.Sin(timeCnt)*animScale;
        timeCnt += (float)0.01;
        // スケールを変更（等倍スケーリング）
        imageRect.sizeDelta = imageScale * scale;

    }
    void AnimTex(TextMeshProUGUI texUI)
    {
        float scale = 1.0f + Mathf.Sin(timeCnt)*animScale;
        texUI.fontSize = 50 * scale;
    }
    void SetAllTextColor(TextMeshProUGUI target, Color32 color)
    {
            target.ForceMeshUpdate();
            var textInfo = target.textInfo;

                for (int i = 0; i < textInfo.characterCount; i++)
                {
                    var charInfo = textInfo.characterInfo[i];
                    if (!charInfo.isVisible) continue;
                    int vertexIndex = charInfo.vertexIndex;
                    int matIndex = charInfo.materialReferenceIndex;
                    var colors = textInfo.meshInfo[matIndex].colors32;

                    for (int j = 0; j < 4; j++)
                        colors[vertexIndex + j] = color;
                 }
        target.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MenuUI : MonoBehaviour
//{
//    private RectTransform imageRect1;
//    private RectTransform imageRect2;
//    // Start is called before the first frame update
//    void Start()
//    {
//        imageRect1 = transform.Find("M_BackGroundUI/M_CloseUI").GetComponent<RectTransform>();
//        imageRect2 = transform.Find("M_BackGroundUI/M_QuitUI").GetComponent<RectTransform>();
//        // UI初期化
//        imageRect1.anchoredPosition = new Vector2(0, 100);
//        imageRect1.sizeDelta = new Vector2(500, 150);
//        imageRect2.anchoredPosition = new Vector2(0, -80);
//        imageRect2.sizeDelta = new Vector2(400, 120);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//    // 表示関数
//    public void ShowPanel()
//    {
//        GameObject obj = GameObject.Find("Menu");

//        Transform panel = obj.transform.Find("M_BackGroundUI");

//        panel.gameObject.SetActive(true);
//    }

//    // 非表示関数
//    public void HidePanel()
//    {
//        GameObject obj = GameObject.Find("Menu");

//        Transform panel = obj.transform.Find("M_BackGroundUI");

//        panel.gameObject.SetActive(false);
//    }
//    // UI強調表示
//    public void SelectUI(int select)
//    {
//        switch (select)
//        {
//            case 0:
//                // ContineuImageの位置とサイズ変更
//                // 位置を変更
//                imageRect1.anchoredPosition = new Vector2(0, 100);
//                // サイズを変更
//                imageRect1.sizeDelta = new Vector2(500, 150);
//                //　QuitImageの位置とサイズを変更
//                imageRect2.anchoredPosition = new Vector2(0, -80);
//                imageRect2.sizeDelta = new Vector2(400, 120);
//                break;
//            case 1:
//                imageRect2.anchoredPosition = new Vector2(0, -100);
//                imageRect2.sizeDelta = new Vector2(500, 150);

//                imageRect1.anchoredPosition = new Vector2(0, 80);
//                imageRect1.sizeDelta = new Vector2(400, 120);
//                break;
//        }
//    }
//}
