using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tutorial_Describe : MonoBehaviour
{

    private Image targetImage; // UI��Image

    // Start is called before the first frame update
    void Start()
    {
        targetImage = GameObject.Find("T_GimmickDescribe").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        SetDescribeNumber(1);
    }
    public void SetDescribeNumber(int number)
    {
        // �M�~�b�N�̉摜�f�ނ�Assets/Resource/Gimmicks_D�t�H���_����"GimmickDescribe_�ԍ�"�̂悤�ɕۑ�����
        // �t�@�C������g�ݗ���(��FGimmickDescribe_1)
        string path = $"Gimmicks_D/GimmickDescribe_{number}";

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
//private UI_Tutorial_Describe iconSetter_D;
//
// Start��
//  GameObject iconR = GameObject.Find("T_GimmickDescribe");
//
// Update��
// if(�Ăяo���C�ӂ̏���)
// {
//     iconSetter_D.SetDescribeNumber(1);
// }
//

