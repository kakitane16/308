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
    public float forceStrength; // 前方向への飛ぶ力
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
        //打ち出すまでの間だけ入る
        if (!isShot)
        {
            ShotAngle();
            Shot();
        }
    }
     
    private void ShotAngle()
    {
        //角度指定
        if (Keyboard.current.aKey.isPressed)
        {
            transform.Rotate(new Vector3(0, -RotateY, 0));
            Debug.Log("Aキーが押されているよ");
        }
        if (Keyboard.current.dKey.isPressed)
        {
            transform.Rotate(new Vector3(0, RotateY, 0));
            Debug.Log("Dキーが押されているよ");
        }
    }

    private void Shot()
    {
        //打つ時のでかさを貯める
        if (Keyboard.current.spaceKey.isPressed)
        {
            Debug.Log("スペースキーが押されているよ");
            if (forceStrength <= 20.0f)
            {
                forceStrength += 0.01f;
            }
        }
        //打ち出し
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            Debug.Log("スペースキーが離されました");
            rb.useGravity = true;
            isShot = true;
            // **前方+上方向へ飛ばす(オブジェクトの質量と関係しているためUnity側で計算させている)**
            Vector3 launchForce = transform.forward * forceStrength + Vector3.up * jumpPower;
            rb.AddForce(launchForce, ForceMode.Impulse);
        }
    }
}