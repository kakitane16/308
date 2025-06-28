using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Tutorial : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            HidePanel();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ShowPanel();
        }
    }

    // �\���֐�
    void ShowPanel()
    {
        GameObject obj = GameObject.Find("Tutorial");

        Transform panel = obj.transform.Find("T_BackGroundUI");

        panel.gameObject.SetActive(true);
    }

    // ��\���֐�
    void HidePanel()
    {
        GameObject obj = GameObject.Find("Tutorial");

        Transform panel = obj.transform.Find("T_BackGroundUI");

        panel.gameObject.SetActive(false);
    }
}
//------------�֐��̌Ăяo��-------------//
//private UI_Tutorial UISetter;
//
// Start��
//  GameObject TutorialUI = GameObject.Find("Tutorial");
//
// Update��
// if(�Ăяo���C�ӂ̏���)
// {
//     UISetter.ShowPanel();
// }
//