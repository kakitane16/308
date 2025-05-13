using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    enum MenuState
    {
        CONTINUE,
        CONTROLS,
        AUDIO,
        QUIT,
    }
    enum GamePadInput
    {
        None,// 入力なし

        StartBotton,

        // 十字ボタン
        DpadUp,
        DpadDown,
        DpadLeft,
        DpadRight,

        // 左スティック
        StickUp,
        StickDown,
        StickLeft,
        StickRight,

        ButtonA,        // Aボタン（決定など）
        ButtonB         // Bボタン（キャンセルなど）
    }
    // スティックのしきい値
    private const float deadZone = 0.5f;

    // ボタン離すまで無効
    private bool inputReleased = true;  // ボタンを離したかどうか

    private bool inControls = false;
    private bool inAudio = false;

    private bool isPress = false;

    public GameObject bgMenu;
    public GameObject[] uiObjects = new GameObject[4]; // UI_Continue 〜 UI_Quit

    public GameObject bgControls;
    public GameObject bgAudio;


    public RectTransform[] rt = new RectTransform[4];  // UIの位置・サイズ用

    private bool g_EndFlag = true; // メニュー終了フラグ
    private MenuState g_MenuState = MenuState.CONTINUE; //初期選択項目(Continue)

    void Start()
    {

        // 初期位置調整
        if (rt[0]) rt[0].anchoredPosition += new Vector2(0, 200);
        if (rt[1]) rt[1].anchoredPosition += new Vector2(0, 50);
        if (rt[2]) rt[2].anchoredPosition += new Vector2(0, -100);
        if (rt[3]) rt[3].anchoredPosition += new Vector2(0, -250);

        // 初期非表示
        SetMenuActive(false);
        SetControlsActive(false);
        SetAudioActive(false);
    }

    void Update()
    {
        // メニュー開始判定
        if (Input.GetKeyDown(KeyCode.M) || GamePadConfirm() == GamePadInput.StartBotton)
        {
            inputReleased = false;
            g_EndFlag = false;
            SetMenuActive(true); //メニュー画面表示&起動
        }


        if (!g_EndFlag) // メニュー実行判定
        {
            // メニュー画面で行う処理たち
            SelectControl(); // 入力に対する操作
            SelectConfirm(); // 決定操作
            Draw(); // 描画
        }

        if (g_EndFlag && inputReleased)
        {
            SetMenuActive(false); //メニュー画面非表示&停止
            g_MenuState = MenuState.CONTINUE; // 初期選択項目リセット
        }

        // 長押し防止リセット
        if (!Input.GetKey(KeyCode.Escape) && !Input.GetKey(KeyCode.Return) && !Input.GetKey(KeyCode.M) &&
            !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            inputReleased = true;
        }
    }

    // 入力に対する操作の関数
    private void SelectControl()
    {
        if (!inputReleased) return;

        // キーボード十字キーかゲームパッド十字ボタン・左スティックで選択項目を変更(上下のみ)
        if (Input.GetKeyDown(KeyCode.UpArrow) ||
            GamePadConfirm() == GamePadInput.DpadUp ||
            GamePadConfirm() == GamePadInput.StickUp)
        {
            inputReleased = false;
            g_MenuState = g_MenuState == MenuState.CONTINUE ? MenuState.QUIT : g_MenuState - 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) ||
            GamePadConfirm() == GamePadInput.DpadDown ||
            GamePadConfirm() == GamePadInput.StickDown)
        {
            inputReleased = false;
            g_MenuState = g_MenuState == MenuState.QUIT ? MenuState.CONTINUE : g_MenuState + 1;
        }
    }

    // 選択項目に決定する関数
    private void SelectConfirm()
    {
        if (!inputReleased) return;
        // キーボードエスケープキーかゲームパッドスタートボタン・Bボタンでメニュー終了フラグを立てる
        if (!inControls && !inAudio &&
            Input.GetKeyDown(KeyCode.Escape) ||
            GamePadConfirm() == GamePadInput.StartBotton ||
            GamePadConfirm() == GamePadInput.ButtonB)
        {
            inputReleased = false; // 長押し防止
            g_EndFlag = true;
            return;


        }

        // キーボードエンターキーかゲームパッドAボタンで現在の選択項目に決定
        if (GamePadConfirm() == GamePadInput.ButtonA)
        {
            isPress = true;
        }
        else
        {
            isPress = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) || isPress)
        {
            inputReleased = false; // 長押し防止
            // 各選択項目による処理
            switch (g_MenuState)
            {
                case MenuState.CONTINUE: // メニュー終了
                    g_EndFlag = true;
                    break;
                case MenuState.CONTROLS: // 操作説明画面
                    inControls = true;
                    SetMenuActive(false);
                    SetControlsActive(true);

                    break;
                case MenuState.AUDIO: // 音量設定画面
                    inAudio = true;
                    SetMenuActive(false);
                    SetAudioActive(true);

                    break;
                case MenuState.QUIT: // ゲーム終了(未実装)
                    break;
            }
            return;
        }
        // 操作説明画面・音量設定画面からメニュー画面へ戻るための判定
        // キーボードエスケープキーかゲームパッドスタートボタン・Bボタンで判定する
        if (GamePadConfirm() == GamePadInput.StartBotton || GamePadConfirm() == GamePadInput.ButtonB)
        {
            isPress = true;
        }
        else
        {
            isPress = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) Debug.Log("puressBotton");
        if (inputReleased && (inControls == true || inAudio == true) && Input.GetKeyDown(KeyCode.Escape) || isPress)
        {
            inputReleased = false; // 長押し防止
            inControls = false;
            inAudio = false;
            SetControlsActive(false);
            SetAudioActive(false);
            SetMenuActive(true);
            Debug.Log("実行");

        }

    }

    // 描画関数
    private void Draw()
    {
        // 表示対象のUIを強調(スケールの変更)
        for (int i = 0; i < rt.Length; i++)
        {
            if (rt[i] == null) continue;
            rt[i].localScale = (i == (int)g_MenuState) ?
                new Vector3(0.8f, 1.2f, 1f) :
                new Vector3(0.5f, 0.8f, 1f);
        }
    }

    // メニューのオンオフ切替する関数
    private void SetMenuActive(bool isActive)
    {
        if (bgMenu != null) bgMenu.SetActive(isActive);
        foreach (var obj in uiObjects)
        {
            if (obj != null) obj.SetActive(isActive);
        }
    }

    // 操作説明画面のオンオフ切替する関数
    private void SetControlsActive(bool isActive)
    {
        if (bgControls != null) bgControls.SetActive(isActive);

        // この画面でUIを使うときに必要な処理
        //foreach (var obj in uiControlsObjects)
        //{
        //    if (obj != null) obj.SetActive()
        //}
    }

    // 音量設定画面のオンオフ切替する関数
    private void SetAudioActive(bool isActive)
    {
        if (bgAudio != null) bgAudio.SetActive(isActive);

        // この画面でUIを使うときに必要な処理
        //foreach (var obj in uiControlsObjects)
        //{
        //    if (obj != null) obj.SetActive()
        //}
    }

    // ゲームパッドの入力を判定する関数
    private GamePadInput GamePadConfirm()
    {
        // 初期化&接続判定
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            // Debug.Log("ゲームパッドが接続されていません");
            return GamePadInput.None;
        }

        // スタートボタンの入力判定
        if (Gamepad.current.startButton.wasPressedThisFrame)
        {
            return GamePadInput.StartBotton;
        }
        // 十字ボタンの入力を判定
        if (Gamepad.current.dpad.up.wasPressedThisFrame)
        {
            return GamePadInput.DpadUp;
        }
        if (Gamepad.current.dpad.down.wasPressedThisFrame)
        {
            return GamePadInput.DpadDown;
        }
        if (Gamepad.current.dpad.left.wasPressedThisFrame)
        {
            return GamePadInput.DpadLeft;
        }
        if (Gamepad.current.dpad.right.wasPressedThisFrame)
        {
            return GamePadInput.DpadRight;
        }

        // 左スティック初期化&方向判定
        Vector2 stick = gamepad.leftStick.ReadValue();
        if (stick.y > deadZone) return GamePadInput.StickUp;
        if (stick.y < -deadZone) return GamePadInput.StickDown;
        if (stick.x < -deadZone) return GamePadInput.StickLeft;
        if (stick.x > deadZone) return GamePadInput.StickRight;

        // Aボタン入力判定（buttonSouth）
        if (gamepad.buttonSouth.wasPressedThisFrame)
        {
            inputReleased = false; // 長押し防止
            return GamePadInput.ButtonA;
        }

        // Bボタン入力判定（buttonEast）
        if (gamepad.buttonEast.wasPressedThisFrame)
        {
            inputReleased = false; // 長押し防止
            return GamePadInput.ButtonB;
        }

        // 入力なし判定
        return GamePadInput.None;

    }
}

