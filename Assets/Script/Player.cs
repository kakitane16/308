using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using UnityEditorInternal.VR;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Image GaugeImage; // ゲージ画像アタッチ
    private GamePadCommand inputChecker;
    private ArmAnimation arm;
    public Arrow arw;
    public Parabola parabola;
    public string ArrowTag = "Arrow"; // アローのターゲットタグ
    private GamePadCommand command;
    public Vector3 velocity;
    private float MaxPower = 20f;
    private float MinPower = 0.0f;
    public float RotateSpeed;
    public bool isShot;
    public float shotpower;
    public float SAngleY;
    public float forceStrength;            // 前方向への飛ぶ力
    private bool sceneJustChanged = true;  //後で使うから消さないで
    public float rotateAgl;
    private float Vertical;   //UIの高さを変更
    private float Horizontal; //UIの横の移動値を変更
    private float Move;       //UIの横と高さの値変更幅

    private int GetInputOB;    //使う物を取得　今調整段階なため最初にここに数値を入れれば変わる
                              //ない　０　ゲームパッド　１　キーボード　２
    private float inputBlockTime = 0.1f; // 入力ブロック時間
    private float sceneStartTime;       // シーンが開始した時間
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
                Debug.LogWarning("オブジェクトが見つかりませんでした");
            }
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
        // シーン切り替え直後の1秒間は入力を受け付けない
        if (Time.time - sceneStartTime < inputBlockTime)
            return;
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
        if (command.UpAction(GetInputOB))
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
        if (command.DownAction(GetInputOB))
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
        if (!command.UpAction(GetInputOB) && !command.DownAction(GetInputOB))
        {
            Vertical = 0.0f;
            Horizontal = 0.0f;
        }
    }


    //***** 打つでかさを保持し、ボタンが離されたら打てる状態になったことをPowerShotingに伝える *****
    private void Shot()
    {
        //打つ時のでかさを貯める
        if (command.IsBbutton(GetInputOB))
        {
            if (parabola != null)
            {
                parabola.ShowParabora();
            }
            Debug.Log("スペースキーが押されているよ");
            //最大値まで戻る場合
            if (forceStrength < MaxPower)
            {
                forceStrength += shotpower;
            }
            else if(forceStrength >=  MaxPower)
            {
                //最大値に達した
                IsReturn = true;
                forceStrength = 0.0f;
            }
        }
        //打ち出し
        if (command.WasBbutton(GetInputOB))
        {
            PowerShoting();
            if (parabola != null)
            {
                parabola.HideDots();
            }
        }
    }

    //打ち出されるの大きさの可視化（ゲージ）
    public void UpdateGauge()
    {
        // ゲージ割合
        float GaugeAmount = Mathf.Clamp01(forceStrength / MaxPower);
        GaugeImage.fillAmount = GaugeAmount;
    }

    //打ち出される角度の可視化（魚のアロー）
    public void UpdateArrow()
    {
        Vector2 currentPosition = arw.GetComponent<RectTransform>().anchoredPosition;  //S 現在の位置を取得
        Vector2 offset = new Vector2(Vertical, Horizontal);                            // 追加したいオフセット値
        arw.GetComponent<RectTransform>().anchoredPosition = currentPosition + offset; // 位置を更新
        arw.transform.rotation = Quaternion.Euler(0, 0, rotateAgl);                    //UIの回転
    }


    //全てを加味し打ち出された時の大きさの計算
    private void PowerShoting()
    {
        Debug.Log("スペースキー or gamepad.b が離されました");
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