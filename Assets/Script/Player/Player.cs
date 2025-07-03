using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Image GaugeImage; // ゲージ画像アタッチ
    public Image IconImage; // ゲージアイコン画像
    public Sprite MaxIcon;
    public Sprite NormalIcon;
    public RectTransform iconRectTransform; // アイコン位置
    public RectTransform gaugeRectTransform; // ゲージ位置
    public GameManager manager;
    public float iconMoveSpeed = 2f;
    // ★追加：威力を保持するための変数
    public float currentPower = 0f;
    private Vector2 iconStartPos; // アイコンの初期位置
    private bool isMaxIconActive = false;
    private bool isMaxIconDelayActive = false; // 最大アイコン維持中フラグ
    private float maxIconDelayTime = 0.5f;    // 最大アイコン維持時間（秒）
    private float maxIconTimer = 0f;          // タイマー
    private GamePadCommand inputChecker;
    public Arrow arw;
    public Parabola parabola;
    public string ArrowTag = "Arrow"; // アローのターゲットタグ
    private GamePadCommand command;
    public Vector3 velocity;
    private float MaxPower = 20f;
    public float RotateSpeed;
    public bool isShot;
    public float shotpower;
    public float SAngleY;
    public float forceStrength;            // 前方向への飛ぶ力
    public float rotateAgl;
    private float Vertical;   //UIの高さを変更
    private float Horizontal; //UIの横の移動値を変更
    private float Move;       //UIの横と高さの値変更幅

    private int GetInputOB;    //使う物を取得　今調整段階なため最初にここに数値を入れれば変わる
                               //ない　０　ゲームパッド　１　キーボード　２
    private float inputBlockTime = 0.1f; // 入力ブロック時間
    private float sceneStartTime;       // シーンが開始した時間
    private bool IsReturn;

    private bool IsReady = false; // 準備完了フラグ
    private bool wasShotReady = false;

    public float lastShotPower;

    // Start is called before the first frame update
    void OnEnable() // OnEnableに変更しました、Start時ではまだ生成されていない可能性があるため
    {
        inputChecker = GetComponent<GamePadCommand>();
        if (inputChecker == null)
        {
            inputChecker = gameObject.AddComponent<GamePadCommand>();
        }
        if (iconRectTransform != null)
        {
            iconStartPos = iconRectTransform.anchoredPosition;
        }
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
        IsReturn = false;
        wasShotReady = false;
        lastShotPower = 0.0f;

        // 以下は現状の開発環境での動作確認用の仮置きです、プレハブ生成版に開発が切り替わった段階で削除してください

        if (manager == null) return;

        IsReady = true; // 準備完了フラグを立てる

        sceneStartTime = Time.time;

        GetInputOB = (int)GameManager.Instance.inputDevice; // 入力デバイスの取得(これはあまり関係ないかもしれませんが、Startでは正常に取得できない可能性を危惧して、こちらに移動しました)
    }

    void LateUpdate()       // オブジェクトの捜索をLateUpdateに移動しました(StartやOnEnableでは正常に取得できなかったため
                            // 一通り処理が終わった後のLateUpdateで毎フレームチェックする方向にしました)
                            // PlayerCamera.cs/Goal.cs/Arrow.cs でも同様に移動しました(記載はしませんが、同様の理由です)
    {
        if (arw != null && manager != null) return;

        // アローのコンポーネントを取得
        if (arw == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(ArrowTag);
            if (player != null) arw = player.GetComponent<Arrow>();
        }

        // マネージャーのコンポーネントを取得
        if (manager == null) manager = GameObject.FindObjectOfType<GameManager>();

        if (manager == null) return;    // 取得に失敗していた場合は弾く

        IsReady = true; // 準備完了フラグを立てる

        sceneStartTime = Time.time; // シーン開始時間を記録(こちらに移動しました、準備が未完了なのに処理が走るのを防ぐため)

        GetInputOB = (int)GameManager.Instance.inputDevice; // 入力デバイスの取得(これはあまり関係ないかもしれませんが、Startでは正常に取得できない可能性を危惧して、こちらに移動しました)
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IsReady)
        {
            rb.velocity = Vector3.zero;
            // *** 緊急的な対処 ***
            // 原因不明の問題の為に、プレハブから生成時に上方への速度が発生してしまっているため
            // 緊急対処として、準備未完了間は速度を0で初期化する処理を組み込んでいます
            // この問題が特定でき、解決できた際にこの処理は削除します

            return; // 準備ができていない場合は何もしない
        }

        // シーン切り替え直後の1秒間は入力を受け付けない
        if (Time.time - sceneStartTime < inputBlockTime)
            return;
        //打ち出すまでの間だけ入る
        if (!isShot)
        {
            //最初は角度を決めてその後打ち出したい威力のタイミングで放つ
            if (!wasShotReady)
            {
                ShotAngle();
            }
            else
            {
                Shot();
                UpdateGauge();
            }
        }
    }

    private void ShotAngle()
    {
        //角度指定
        if (command.UpAction(GetInputOB))
        {
            if (SAngleY < 10)
            {
                SAngleY += 0.5f;
                rotateAgl += RotateSpeed;
                Vertical = -Move;
                Horizontal = Move;
                Debug.Log("Wキーが押されているよ");

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
                Debug.Log("Sキーが押されているよ");

                UpdateArrow();
            }
        }
        if (!command.UpAction(GetInputOB) && !command.DownAction(GetInputOB))
        {
            Vertical = 0.0f;
            Horizontal = 0.0f;
        }
        if(command.WasBbutton(GetInputOB))
        {
            wasShotReady = true;
        }
    }


    //***** 打つでかさを保持し、ボタンが離されたら打てる状態になったことをPowerShotingに伝える *****
    private void Shot()
    {
        //打つ時のでかさを貯める
        if (command.IsBbutton(GetInputOB))
        {
            parabola.ShowParabora();
            Debug.Log("スペースキーが押されているよ");
            //最大値まで戻る場合
            if (forceStrength < MaxPower)
            {
                forceStrength += shotpower;
            }
            else if (forceStrength >= MaxPower)
            {
                //最大値に達した
                IsReturn = true;
                forceStrength = 0.0f;
            }
        }
        //打ち出し
        if (command.WasBbutton(GetInputOB))
        {
            lastShotPower = forceStrength;
            PowerShoting();
            if (parabola != null)
            {
                parabola.HideDots();
            }
        }
    }

    //打ち出されるの大きさの可視化（ゲージ）
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
                // ガクッと瞬間移動するように位置を直接セット
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
            // 最大値に達しているならアイコンをMaxIconにする＆維持タイマーリセット
            if (!isMaxIconActive)
            {
                IconImage.sprite = MaxIcon;
                isMaxIconActive = true;
                Debug.Log("MaxIconに切り替えました");
            }
            // 維持フラグをオフに
            isMaxIconDelayActive = false;
            maxIconTimer = 0f;
        }
        else
        {
            // 最大値じゃなくなった瞬間に維持フラグをオンしてタイマー開始
            if (isMaxIconActive && !isMaxIconDelayActive)
            {
                isMaxIconDelayActive = true;
                maxIconTimer = 0f;
                Debug.Log("最大アイコン維持タイマー開始");
            }

            if (isMaxIconDelayActive)
            {
                maxIconTimer += Time.deltaTime;

                if (maxIconTimer >= maxIconDelayTime)
                {
                    // 維持時間が過ぎたらアイコンを元に戻す
                    IconImage.sprite = NormalIcon;
                    isMaxIconActive = false;
                    isMaxIconDelayActive = false;
                    Debug.Log("NormalIconに戻しました");
                }
            }
            else
            {
                // 維持フラグがないなら普通に戻す
                if (isMaxIconActive)
                {
                    IconImage.sprite = NormalIcon;
                    isMaxIconActive = false;
                    Debug.Log("NormalIconに戻しました");
                }
            }
        }
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
        currentPower = forceStrength;
        // **前方+上方向へ飛ばす(オブジェクトの質量と関係しているためUnity側で計算させている)**
        Vector3 launchForce = transform.forward * forceStrength + Vector3.up * SAngleY;
        rb.AddForce(launchForce, ForceMode.Impulse);

        isShot = true;
        // アローを非表示にする
        arw.gameObject.SetActive(false);
        forceStrength = 0f; // 溜めリセット
    }

    //ギミック側でショットPowerを使うため
    public float GetLastShotPower()
    {
        return currentPower;
    }
}