using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Tutorial : MonoBehaviour
{
    public int gimmickNum;  
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // 表示関数
    public void ShowPanel(int setGimmickNum)
    {
        gimmickNum = setGimmickNum;
        GameObject obj = GameObject.Find("Tutorial");

        Transform panel = obj.transform.Find("T_BackGroundUI");

        panel.gameObject.SetActive(true);

    }

    // 非表示関数
    public void HidePanel()
    {
        GameObject obj = GameObject.Find("Tutorial");

        Transform panel = obj.transform.Find("T_BackGroundUI");

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