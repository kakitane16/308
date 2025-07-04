using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tutorial_Image : MonoBehaviour
{
    private Image targetImage; // UI��Image

    // Start is called before the first frame update
    void Start()
    {
        targetImage = GameObject.Find("T_GimmickImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        SetImageNumber(1);
    }
    public void SetImageNumber(int number)
    {
        // �M�~�b�N�̉摜�f�ނ�Assets/Resource/Gimmicks_I�t�H���_����"GimmickImage_�ԍ�"�̂悤�ɕۑ�����
        // �t�@�C������g�ݗ���(��FGimmickImage_1)
        string path = $"Gimmicks_I/GimmickImage_{number}";

        // Resource����Sprite�����[�h
        Sprite newSprite = Resources.Load<Sprite>(path);

        if (newSprite != null)
        {
            targetImage.sprite = newSprite;
        }
        else
        {
            Debug.Log("�ǂݍ��ݎ��s");
            Debug.LogWarning("�w�肳�ꂽ�摜��������܂���: " + path);
        }
    }
}
//------------�֐��̌Ăяo��-------------//
//private UI_Tutorial_Image iconSetter_I;
//
// Start��
//  GameObject iconL = GameObject.Find("T_GimmickImage");
//
// Update��
// if(�Ăяo���C�ӂ̏���)
// {
//     iconSetter_I.SetImageNumber(1);
// }
//