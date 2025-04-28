using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;

public class Player : MonoBehaviour
{
    public float bounceDamping; // 跳ね返り時の減衰率
    public float MoveX;
    public float MoveY;
    private bool isShot;
    private int counter = 0; //打ち出す方向を簡単に数値で保持する
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
        //タグWallに当たったら跳ね返る
        if (other.gameObject.CompareTag("Wall"))
        {
            Vector3 velocityNext = Vector3.Reflect(velocity, other.contacts[0].normal);
            Debug.Log("velocty"+ velocity);
            rb.velocity = velocityNext;
            velocity = rb.velocity * bounceDamping;//速さなどを減衰させていく
        }
    }

    private void FixedUpdate()
    {
        Shot();
    }

    private void Shot()
    {
        //一度だけ打つ
        if (isShot)
        {
            return;
        }

        ShotAngle();
        WaitShot();
    }

    private void ShotAngle()
    {
        /*一次方程式
        　(MoveX,MoveY)の表ができる*/

        //最初のif文で何で入力しているか判定する　
        //ゲームパッド
        if (Gamepad.current != null)
        {
            var gamepad = Gamepad.current; //ゲームパッドの今の情報を渡す
            //float verticalInput = Gamepad.current.leftStick.y;

            // 上に向いている場合
            //if (verticalInput > 0.5f)
            //{
            //    counter++;
            //    Debug.Log($"Counter: {counter} (Added)");
            //}
            //// 下に向いている場合
            //else if (verticalInput < -0.5f)
            //{
            //    counter--;
            //    Debug.Log($"Counter: {counter} (Subtracted)");
            //}
        }
        //キーボード
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
       
        //角度指定
        velocity = new Vector3(MoveX, MoveY, 0.0f);
    }

    private void WaitShot()
    {
        //最初のif文で何で入力しているか判定する
        //ゲームパッド
        if (Gamepad.current != null)
        {
            if(Gamepad.current.bButton.isPressed)
            {
                rb.velocity = velocity;
                isShot = true;
            }
        }
        //キーボード
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