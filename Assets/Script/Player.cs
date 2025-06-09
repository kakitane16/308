using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using UnityEditorInternal.VR;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Image GaugeImage; // �Q�[�W�摜�A�^�b�`
    public Image IconImage; // �Q�[�W�A�C�R���摜
    public Sprite MaxIcon;
    public Sprite NormalIcon;
    public RectTransform iconRectTransform; // �A�C�R���ʒu
    public RectTransform gaugeRectTransform; // �Q�[�W�ʒu
    public float iconMoveSpeed = 2f;
    private Vector2 iconStartPos; // �A�C�R���̏����ʒu
    private bool isMaxIconActive = false;
    private bool isMaxIconDelayActive = false; // �ő�A�C�R���ێ����t���O
    private float maxIconDelayTime = 0.5f;    // �ő�A�C�R���ێ����ԁi�b�j
    private float maxIconTimer = 0f;          // �^�C�}�[
    private GamePadCommand inputChecker;
    private ArmAnimation arm;
    public Arrow arw;
    public Parabola parabola;
    public string ArrowTag = "Arrow"; // �A���[�̃^�[�Q�b�g�^�O
    private GamePadCommand command;
    public Vector3 velocity;
    private float MaxPower = 20f;
    private float MinPower = 0.0f;
    public float RotateSpeed;
    public bool isShot;
    public float shotpower;
    public float SAngleY;
    public float forceStrength;            // �O�����ւ̔�ԗ�
    private bool sceneJustChanged = true;  //��Ŏg����������Ȃ���
    public float rotateAgl;
    private float Vertical;   //UI�̍�����ύX
    private float Horizontal; //UI�̉��̈ړ��l��ύX
    private float Move;       //UI�̉��ƍ����̒l�ύX��

    private int GetInputOB;    //�g�������擾�@�������i�K�Ȃ��ߍŏ��ɂ����ɐ��l������Ες��
                               //�Ȃ��@�O�@�Q�[���p�b�h�@�P�@�L�[�{�[�h�@�Q
    private float inputBlockTime = 0.1f; // ���̓u���b�N����
    private float sceneStartTime;       // �V�[�����J�n��������
    private bool IsReturn;

    // Start is called before the first frame update
    void Start()
    {
        sceneStartTime = Time.time;
        inputChecker = GetComponent<GamePadCommand>();
        if (inputChecker == null)
        {
            inputChecker = gameObject.AddComponent<GamePadCommand>();
        }
        if (arw == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(ArrowTag);
            if (player != null)
            {
                arw = player.GetComponent<Arrow>();
            }
            else
            {
                Debug.LogWarning("�I�u�W�F�N�g��������܂���ł���");
            }
        }
        if (iconRectTransform != null)
        {
            iconStartPos = iconRectTransform.anchoredPosition;
        }
        rb = GetComponent<Rigidbody>();
        command = new GamePadCommand();
        arm = new ArmAnimation();
        GetInputOB = (int)GameManager.Instance.inputDevice;
        isShot = false;
        rb.useGravity = false;
        SAngleY = 0;
        Vertical = 0.0f;
        Horizontal = 0.0f;
        Move = 0.1f;
        forceStrength = 0.0f;
        Debug.Log(GetInputOB);
        IsReturn = false;
    }
    // Update is called once per frame
    private void Update()
    {
        // �V�[���؂�ւ������1�b�Ԃ͓��͂��󂯕t���Ȃ�
        if (Time.time - sceneStartTime < inputBlockTime)
            return;
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
                SAngleY += 0.5f;
                rotateAgl += RotateSpeed;
                Vertical = -Move;
                Horizontal = Move;
                Debug.Log("W�L�[��������Ă����");

                UpdateArrow();
            }
        }
        if (command.DownAction(GetInputOB))
        {
            if (SAngleY > 0)
            {
                SAngleY -= 0.5f;
                rotateAgl -= RotateSpeed;
                Vertical = Move;
                Horizontal = -Move;
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


    //***** �łł�����ێ����A�{�^���������ꂽ��łĂ��ԂɂȂ������Ƃ�PowerShoting�ɓ`���� *****
    private void Shot()
    {
        //�ł��̂ł����𒙂߂�
        if (command.IsBbutton(GetInputOB))
        {
            if (parabola != null)
            {
                parabola.ShowParabora();
            }
            Debug.Log("�X�y�[�X�L�[��������Ă����");
            //�ő�l�܂Ŗ߂�ꍇ
            if (forceStrength < MaxPower)
            {
                forceStrength += shotpower;
            }
            else if (forceStrength >= MaxPower)
            {
                //�ő�l�ɒB����
                IsReturn = true;
                forceStrength = 0.0f;
            }
        }
        //�ł��o��
        if (command.WasBbutton(GetInputOB))
        {
            PowerShoting();
            if (parabola != null)
            {
                parabola.HideDots();
            }
        }
    }

    //�ł��o�����̑傫���̉����i�Q�[�W�j
    public float iconMoveDistance = 50f;
    public void UpdateGauge()
    {
        float GaugeAmount = Mathf.Clamp01(forceStrength / MaxPower);
        GaugeImage.fillAmount = GaugeAmount;

        if (iconRectTransform != null)
        {
            float offsetX = iconMoveDistance * GaugeAmount;
            if (forceStrength == 0f && isMaxIconActive)
            {
                offsetX -= 5f;
                // �K�N�b�Əu�Ԉړ�����悤�Ɉʒu�𒼐ڃZ�b�g
                iconRectTransform.anchoredPosition = iconStartPos + new Vector2(offsetX, 0);
            }
            else
            {
                Vector2 targetPos = iconStartPos + new Vector2(offsetX, 0);
                iconRectTransform.anchoredPosition = Vector2.Lerp(iconRectTransform.anchoredPosition, targetPos, Time.deltaTime * iconMoveSpeed);
            }
        }

        if (GaugeAmount >= 1.0f)
        {
            // �ő�l�ɒB���Ă���Ȃ�A�C�R����MaxIcon�ɂ��違�ێ��^�C�}�[���Z�b�g
            if (!isMaxIconActive)
            {
                IconImage.sprite = MaxIcon;
                isMaxIconActive = true;
                Debug.Log("MaxIcon�ɐ؂�ւ��܂���");
            }
            // �ێ��t���O���I�t��
            isMaxIconDelayActive = false;
            maxIconTimer = 0f;
        }
        else
        {
            // �ő�l����Ȃ��Ȃ����u�ԂɈێ��t���O���I�����ă^�C�}�[�J�n
            if (isMaxIconActive && !isMaxIconDelayActive)
            {
                isMaxIconDelayActive = true;
                maxIconTimer = 0f;
                Debug.Log("�ő�A�C�R���ێ��^�C�}�[�J�n");
            }

            if (isMaxIconDelayActive)
            {
                maxIconTimer += Time.deltaTime;

                if (maxIconTimer >= maxIconDelayTime)
                {
                    // �ێ����Ԃ��߂�����A�C�R�������ɖ߂�
                    IconImage.sprite = NormalIcon;
                    isMaxIconActive = false;
                    isMaxIconDelayActive = false;
                    Debug.Log("NormalIcon�ɖ߂��܂���");
                }
            }
            else
            {
                // �ێ��t���O���Ȃ��Ȃ畁�ʂɖ߂�
                if (isMaxIconActive)
                {
                    IconImage.sprite = NormalIcon;
                    isMaxIconActive = false;
                    Debug.Log("NormalIcon�ɖ߂��܂���");
                }
            }
        }
    }



    //�ł��o�����p�x�̉����i���̃A���[�j
    public void UpdateArrow()
    {
        Vector2 currentPosition = arw.GetComponent<RectTransform>().anchoredPosition;  //S ���݂̈ʒu���擾
        Vector2 offset = new Vector2(Vertical, Horizontal);                            // �ǉ��������I�t�Z�b�g�l
        arw.GetComponent<RectTransform>().anchoredPosition = currentPosition + offset; // �ʒu���X�V
        arw.transform.rotation = Quaternion.Euler(0, 0, rotateAgl);                    //UI�̉�]
    }


    //�S�Ă��������ł��o���ꂽ���̑傫���̌v�Z
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
}