using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;

public class Player : MonoBehaviour
{
    public float bounceDamping; // ���˕Ԃ莞�̌�����
    public float MoveX;
    public float MoveY;
    private bool isShot;
    private int counter = 0; //�ł��o���������ȒP�ɐ��l�ŕێ�����
    Rigidbody    rb;
    private Vector3 velocity;
        
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MoveX  = -15.0f;
        MoveY  = 10.0f;
        isShot = false;
    }
    // Update is called once per frame
    private void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        //�^�OWall�ɓ��������璵�˕Ԃ�
        if (other.gameObject.CompareTag("Wall"))
        {
            Vector3 velocityNext = Vector3.Reflect(velocity, other.contacts[0].normal);
            Debug.Log("velocty"+ velocity);
            rb.velocity = velocityNext;
            velocity = rb.velocity * bounceDamping;//�����Ȃǂ����������Ă���
        }
    }

    private void FixedUpdate()
    {
        Shot();
    }

    private void Shot()
    {
        //��x�����ł�
        if (isShot)
        {
            return;
        }

        ShotAngle();
        WaitShot();
    }

    private void ShotAngle()
    {
        /*�ꎟ������
        �@(MoveX,MoveY)�̕\���ł���*/

        //�ŏ���if���ŉ��œ��͂��Ă��邩���肷��@
        //�Q�[���p�b�h
        if (Gamepad.current != null)
        {
            var gamepad = Gamepad.current; //�Q�[���p�b�h�̍��̏���n��
            //float verticalInput = Gamepad.current.leftStick.y;

            // ��Ɍ����Ă���ꍇ
            //if (verticalInput > 0.5f)
            //{
            //    counter++;
            //    Debug.Log($"Counter: {counter} (Added)");
            //}
            //// ���Ɍ����Ă���ꍇ
            //else if (verticalInput < -0.5f)
            //{
            //    counter--;
            //    Debug.Log($"Counter: {counter} (Subtracted)");
            //}
        }
        //�L�[�{�[�h
        if (Keyboard.current != null)
        {
            if (MoveX >= -20)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    MoveX -= 2;
                    MoveY += 1;
                }
            }
            if (MoveX <= 15)
            {
                if (Input.GetKey(KeyCode.S))
                {
                    MoveX += 2;
                    MoveY -= 1;
                }
            }
        }
       
        //�p�x�w��
        velocity = new Vector3(MoveX, MoveY, 0.0f);
    }

    private void WaitShot()
    {
        //�ŏ���if���ŉ��œ��͂��Ă��邩���肷��
        //�Q�[���p�b�h
        if (Gamepad.current != null)
        {
            if(Gamepad.current.bButton.isPressed)
            {
                rb.velocity = velocity;
                isShot = true;
            }
        }
        //�L�[�{�[�h
        if (Keyboard.current != null)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = velocity;
                isShot = true;
            }
        }
    }
}