using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public Image GaugeImage; // ゲージ画像アタッチ
    public Arrow arw;
    public Vector3 velocity;
    private float MaxPower = 20f;
    public float RotateSpeed;
    private bool isShot;
    public float shotpower;
    public float SAngleY;
    public float forceStrength;            // 前方向への飛ぶ力
    private bool sceneJustChanged = true;  //後で使うから消さないで
    public float rotateAgl;
    private float Vertical;   //UIの高さを変更
    private float Horizontal; //UIの横の移動値を変更
    private float Move;       //UIの横と高さの値変更幅

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isShot = false;
        rb.useGravity = false;
        SAngleY = 0;
        Vertical = 0.0f;
        Horizontal = 0.0f;
        Move = 0.1f;
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
                SAngleY    += 0.5f;
                rotateAgl  += RotateSpeed; 
                Vertical    = -Move;
                Horizontal  = Move;
                Debug.Log("Wキーが押されているよ");

                UpdateArrow();
            }
        }
        if (Keyboard.current.sKey.isPressed)
        {
            if (SAngleY > 0)
            {
                SAngleY    -= 0.5f;
                rotateAgl  -= RotateSpeed;
                Vertical    = Move;
                Horizontal  = -Move;
                Debug.Log("Sキーが押されているよ");

                UpdateArrow();
            }
        }
        if(Keyboard.current.sKey.wasReleasedThisFrame || Keyboard.current.sKey.wasReleasedThisFrame)
        {
            Vertical   = 0.0f;
            Horizontal = 0.0f;
        }
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
        Vector2 currentPosition = arw.GetComponent<RectTransform>().anchoredPosition;  // 現在の位置を取得
        Vector2 offset = new Vector2(Vertical, Horizontal);                            // 追加したいオフセット値
        arw.GetComponent<RectTransform>().anchoredPosition = currentPosition + offset; // 位置を更新
        arw.transform.rotation = Quaternion.Euler(0, 0, rotateAgl); //UIの回転
    }
    public void ResetSceneFlag()
    {
        sceneJustChanged = false;
    }
}