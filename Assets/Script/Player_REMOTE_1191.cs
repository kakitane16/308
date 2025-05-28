using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public Image GaugeImage; // �Q�[�W�摜�A�^�b�`
    public Arrow arw;
    public string PlayerTag = "Player"; // �^�[�Q�b�g�̃^�O
    public GamePadCommand command;
    public Vector3 velocity;
    private float MaxPower = 20f;
    public float RotateSpeed;
    private bool isShot;
    public float shotpower;
    public float SAngleY;
    private float forceStrength;            // �O�����ւ̔�ԗ�
    private bool sceneJustChanged = true;  //��Ŏg����������Ȃ���
    public float rotateAgl;
    private float Vertical;   //UI�̍�����ύX
    private float Horizontal; //UI�̉��̈ړ��l��ύX
    private float Move;       //UI�̉��ƍ����̒l�ύX��

    public int GetInputOB;    //�g�������擾�@�������i�K�Ȃ��ߍŏ��ɂ����ɐ��l������Ες��
                              //�Ȃ��@�O�@�Q�[���p�b�h�@�P�@�L�[�{�[�h�@�Q

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        command = new GamePadCommand();
        isShot = false;
        rb.useGravity = false;
        SAngleY = 0;
        Vertical = 0.0f;
        Horizontal = 0.0f;
        Move = 0.1f;
        forceStrength = 0.0f;
        Debug.Log(GetInputOB);

        if (arw == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PlayerTag);
            if (player != null)
            {
                arw = player.GetComponent<Arrow>();
            }
            else
            {
                Debug.LogWarning("�I�u�W�F�N�g��������܂���ł���");
            }
        }
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
        if (command.UpAction(GetInputOB))
        {
            if (SAngleY < 10)
            {
                SAngleY    += 0.5f;
                rotateAgl  += RotateSpeed; 
                Vertical    = -Move;
                Horizontal  = Move;
                Debug.Log("W�L�[��������Ă����");

                UpdateArrow();
            }
        }
        if (command.DownAction(GetInputOB))
        {
            if (SAngleY > 0)
            {
                SAngleY    -= 0.5f;
                rotateAgl  -= RotateSpeed;
                Vertical    = Move;
                Horizontal  = -Move;
                Debug.Log("S�L�[��������Ă����");

                UpdateArrow();
            }
        }
        if (!command.UpAction(GetInputOB) && !command.DownAction(GetInputOB))
        {
            Vertical = 0.0f;
            Horizontal = 0.0f;
        }
    }

    private void Shot()
    {
        //�ł��̂ł����𒙂߂�
        if (command.IsBbutton(GetInputOB))
        {
            Debug.Log("�X�y�[�X�L�[��������Ă����");
            if (forceStrength < MaxPower)
            {
                forceStrength += shotpower;
            }
        }
        //�ł��o��
        if (command.WasBbutton(GetInputOB))
        {
            PowerShoting();
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
        Vector2 currentPosition = arw.GetComponent<RectTransform>().anchoredPosition;  // ���݂̈ʒu���擾
        Vector2 offset = new Vector2(Vertical, Horizontal);                            // �ǉ��������I�t�Z�b�g�l
        arw.GetComponent<RectTransform>().anchoredPosition = currentPosition + offset; // �ʒu���X�V
        arw.transform.rotation = Quaternion.Euler(0, 0, rotateAgl);                    //UI�̉�]
    }

    //�ł��o���ꂽ���̑傫���̌v�Z
    private void PowerShoting()
    {
        Debug.Log("�X�y�[�X�L�[ or gamepad.b ��������܂���");
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

    public void ResetSceneFlag()
    {
        sceneJustChanged = false;
    }
}