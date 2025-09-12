using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームパッドまたはキーボードでタイトル画面のボタン選択を行うスクリプト。
/// UIタイトルのボタンを上下に移動して、決定ボタンで選択中のボタンの処理を実行します。
/// </summary>


public class UI_R_Button : MonoBehaviour
{
    public List<Button> menuButtons; // UIでボタン登録
    private GamePadCommand _command;
    private int GetInputOB;
    private int selectedIndex = 0;

    private float inputCooldown = 0.25f; // 選択移動の受付間隔
    private float inputCooldownTimer = 0f;

    private bool confirmButtonReleased = true; // 決定ボタンが離された状態かどう

    public Button buttonLeft;//
    public Button buttonRight;//対象のボタン

    public Sprite leftSpriteBad;
    public Sprite leftSpriteNormal;
    public Sprite leftSpritePerfect;

    public Sprite rightSpriteBad;
    public Sprite rightSpriteNormal;
    public Sprite rightSpritePerfect;


    private void Start()
    {
        //int score = GameManager.Instance.score;
        UpdateButtonImage(GameManager.Instance.score);//ランクによってボタンの画像を切り替える

        _command = FindObjectOfType<GamePadCommand>();
        GetInputOB = (int)GameManager.Instance.inputDevice;

        if (menuButtons.Count > 0)
        {
           // SelectButton(0); // 最初のボタンを選択状態に

            StartCoroutine(SelectFirstButtonNextFrame());
        }

    }

    private System.Collections.IEnumerator SelectFirstButtonNextFrame()
    {
        yield return null; // EventSystem 初期化待ち
        yield return null;

        var buttonObj = menuButtons[0].gameObject;
        SelectButton(0);

        // ButtonSize を直接呼び出す
        var buttonSize = buttonObj.GetComponent<ButtonSize>();
        if (buttonSize != null)
        {
            buttonSize.OnSelect(new BaseEventData(EventSystem.current));
        }

        Debug.Log("最初のボタンを選択して OnSelect を発火: " + menuButtons[0].name);
    }


    private void Update()
    {
        // クールダウン中は入力を受け付けないように
        if (inputCooldownTimer > 0f)
        {
            inputCooldownTimer -= Time.deltaTime;
            return;
        }

        // 縦方向の入力を取得
        float vertical = _command.GetVerticalAxis(GetInputOB);

        if (_command.LeftAction(GetInputOB))
        {
            inputCooldownTimer = inputCooldown;
            selectedIndex = (selectedIndex + (-1) + menuButtons.Count) % menuButtons.Count;
            SelectButton(selectedIndex);
        }
        if (_command.RightAction(GetInputOB))
        {
            inputCooldownTimer = inputCooldown;
            selectedIndex = (selectedIndex + (1) + menuButtons.Count) % menuButtons.Count;
            SelectButton(selectedIndex);
        }

        HandleDecision();
    }
    // ====== 指定インデックスのボタンを選択状態に ======
    private void SelectButton(int index)
    {
        if (menuButtons != null && menuButtons.Count > 0)
        {
            var buttonObj = menuButtons[index].gameObject;

            // 一度選択をクリア
            EventSystem.current.SetSelectedGameObject(null);
            // 次のフレームで再選択
            EventSystem.current.SetSelectedGameObject(buttonObj);
        }
    }

    // ====== 決定ボタンが押されたときに、現在選択中のボタン実行 ======
    private void HandleDecision()
    {
        if (!_command.IsBbutton(GetInputOB))
        {
            confirmButtonReleased = true;
        }

        if (_command.IsBbutton(GetInputOB) && confirmButtonReleased)
        {
            confirmButtonReleased = false;
            menuButtons[selectedIndex].onClick.Invoke();
        }
    }

    //スコアによってボタンの表示を切り替える
    public void UpdateButtonImage(int rank)
    {
        Sprite leftSprite;
        Sprite rightSprite;

        switch(rank)
        {
            case (int)review.Bad:
                leftSprite = leftSpriteBad;
                rightSprite = rightSpriteBad;
                break;
            case (int)review.Nomal:
                leftSprite = leftSpriteNormal;
                rightSprite = rightSpriteNormal;
                break;
            case (int)review.Perfect:
                leftSprite = leftSpritePerfect;
                rightSprite = rightSpritePerfect;
                break;
            default:
                leftSprite = leftSpriteBad;
                rightSprite = rightSpriteBad;
                break;
        }

        if (buttonLeft != null) buttonLeft.image.sprite = leftSprite;
        if (buttonRight != null) buttonRight.image.sprite = rightSprite;
    }
}
