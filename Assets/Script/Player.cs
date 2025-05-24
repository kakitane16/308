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
    public Arrow arw;
    private float MaxPower = 20f;

    public float MoveX;
    public float RotateY;
    public float RotateSpeed;
    public float jumpPower;
    private bool isShot;
    public float shotpower;
    public float SAngleY;
    public float forceStrength; // �O�����ւ̔�ԗ�
    Rigidbody    rb;
    private bool sceneJustChanged = true;
    public Vector3 velocity;
    float rotateAgl;

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
                rotateAgl += RotateSpeed;
                Debug.Log("W�L�[��������Ă����");
            }
        }
        if (Keyboard.current.sKey.isPressed)
        {
            if (SAngleY > 0)
            {
                SAngleY -= 0.5f;
                rotateAgl -= RotateSpeed;
                Debug.Log("S�L�[��������Ă����");
            }
        }
        UpdateArrow();
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
            // �A���[���\���ɂ���
            arw.gameObject.SetActive(false);
            forceStrength = 0f; // ���߃��Z�b�g
        }
    }
    public void UpdateGauge()
    {
        // �Q�[�W����
        float GaugeAmount = Mathf.Clamp01(forceStrength / MaxPower);
        GaugeImage.fillAmount = GaugeAmount;
    }

    public void UpdateArrow()
    {
        arw.transform.rotation = Quaternion.Euler(0, 0, rotateAgl); 
    }
    public void ResetSceneFlag()
    {
        sceneJustChanged = false;
    }
}