using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;

public class Player : MonoBehaviour
{
    public float MoveX;
    public float RotateY;
    public float jumpPower;
    private bool isShot;
    public float forceStrength; // �O�����ւ̔�ԗ�
    Rigidbody    rb;
        
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isShot = false;
        rb.useGravity = false;
    }
    // Update is called once per frame
    private void Update()
    {
        //�ł��o���܂ł̊Ԃ�������
        if (!isShot)
        {
            ShotAngle();
            Shot();
        }
    }
     
    private void ShotAngle()
    {
        //�p�x�w��
        if (Keyboard.current.aKey.isPressed)
        {
            transform.Rotate(new Vector3(0, -RotateY, 0));
            Debug.Log("A�L�[��������Ă����");
        }
        if (Keyboard.current.dKey.isPressed)
        {
            transform.Rotate(new Vector3(0, RotateY, 0));
            Debug.Log("D�L�[��������Ă����");
        }
    }

    private void Shot()
    {
        //�ł��̂ł����𒙂߂�
        if (Keyboard.current.spaceKey.isPressed)
        {
            Debug.Log("�X�y�[�X�L�[��������Ă����");
            if (forceStrength <= 20.0f)
            {
                forceStrength += 0.01f;
            }
        }
        //�ł��o��
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            Debug.Log("�X�y�[�X�L�[��������܂���");
            rb.useGravity = true;
            isShot = true;
            // **�O��+������֔�΂�(�I�u�W�F�N�g�̎��ʂƊ֌W���Ă��邽��Unity���Ōv�Z�����Ă���)**
            Vector3 launchForce = transform.forward * forceStrength + Vector3.up * jumpPower;
            rb.AddForce(launchForce, ForceMode.Impulse);
        }
    }
}