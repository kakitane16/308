using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // ゲージ関連
    public Image GaugeImage; // ゲージ画像アタッチ
    public Arrow arw;
    private float MaxPower = 20f;

    public float MoveX;
    public float RotateY;
    public float RotateSpeed;
    public float jumpPower;
    private bool isShot;
    public float shotpower;
    public float SAngleY;
    public float forceStrength; // 前方向への飛ぶ力
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
        //打ち出すまでの間だけ入る
        if (!isShot)
        {
            ShotAngle();
            Shot();
            UpdateGauge();
        }
    }
     
    private void ShotAngle()
    {
        //角度指定
        if (Keyboard.current.wKey.isPressed)
        {
            if (SAngleY < 10)
            {
                SAngleY += 0.5f;
                rotateAgl += RotateSpeed;
                Debug.Log("Wキーが押されているよ");
            }
        }
        if (Keyboard.current.sKey.isPressed)
        {
            if (SAngleY > 0)
            {
                SAngleY -= 0.5f;
                rotateAgl -= RotateSpeed;
                Debug.Log("Sキーが押されているよ");
            }
        }
        UpdateArrow();
    }

    private void Shot()
    {
        //打つ時のでかさを貯める
        if (Keyboard.current.spaceKey.isPressed)
        {
            Debug.Log("スペースキーが押されているよ");
            if (forceStrength < MaxPower)
            {
                forceStrength += shotpower;
            }
        }
        //打ち出し
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            Debug.Log("スペースキーが離されました");
            rb.useGravity = true;
            isShot = true;
            // **前方+上方向へ飛ばす(オブジェクトの質量と関係しているためUnity側で計算させている)**
            Vector3 launchForce = transform.forward * forceStrength + Vector3.up * SAngleY;
            rb.AddForce(launchForce, ForceMode.Impulse);

            isShot = true;
            // アローを非表示にする
            arw.gameObject.SetActive(false);
            forceStrength = 0f; // 溜めリセット
        }
    }
    public void UpdateGauge()
    {
        // ゲージ割合
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