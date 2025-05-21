using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // �Q�[�W�֘A
    public Image GaugeImage; // �Q�[�W�摜�A�^�b�`
    private float MaxPower = 10f;

    public float MoveX;
    public float RotateY;
    public float jumpPower;
    private bool isShot;
    public float shotpower;
    public float SAngleY;
    public float forceStrength; // �O�����ւ̔�ԗ�
    Rigidbody    rb;
    private bool sceneJustChanged = true;
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isShot = false;
        rb.useGravity = false;
        SAngleY = 0;
    }
    // Update is called once per frame
    private void Update()
    {
        //�V�[���؂�ւ��̏��񓮍�h�~
        //if(sceneJustChanged)
        //{
        //    return;
        //}
        //�ł��o���܂ł̊Ԃ�������
        if (!isShot)
        {
            ShotAngle();
            Shot();
            UpdateGauge();
        }
    }
     
    private void ShotAngle()
    {
        //�p�x�w��
        if (Keyboard.current.wKey.isPressed)
        {
            if (SAngleY < 10)
            {
                SAngleY += 0.5f;
                Debug.Log("W�L�[��������Ă����");
            }
        }
        if (Keyboard.current.sKey.isPressed)
        {
            if (SAngleY > 0)
            {
                SAngleY -= 0.5f;
                Debug.Log("S�L�[��������Ă����");
            }
        }
    }

    private void Shot()
    {
        //�ł��̂ł����𒙂߂�
        if (Keyboard.current.spaceKey.isPressed)
        {
            Debug.Log("�X�y�[�X�L�[��������Ă����");
            if (forceStrength < MaxPower)
            {
                forceStrength += shotpower;
            }
        }
        //�ł��o��
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            Debug.Log("�X�y�[�X�L�[��������܂���");
            rb.useGravity = true;
            isShot = true;
            // **�O��+������֔�΂�(�I�u�W�F�N�g�̎��ʂƊ֌W���Ă��邽��Unity���Ōv�Z�����Ă���)**
            Vector3 launchForce = transform.forward * forceStrength + Vector3.up * SAngleY;
            rb.AddForce(launchForce, ForceMode.Impulse);

            isShot = true;
            forceStrength = 0f; // ���߃��Z�b�g
        }
    }
    public void UpdateGauge()
    {
        // �Q�[�W����
        float GaugeAmount = Mathf.Clamp01(forceStrength / MaxPower);
        GaugeImage.fillAmount = GaugeAmount;
    }
    public void ResetSceneFlag()
    {
        sceneJustChanged = false;
    }
}