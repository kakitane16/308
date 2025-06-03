using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームパッドまたはキーボードでタイトル画面のボタン選択を行うスクリプト。
/// UIタイトルのボタンを上下に移動して、決定ボタンで選択中のボタンの処理を実行します。
/// </summary>


public class UI_S_StageSelect : MonoBehaviour
{
    public List<Button> menuButtons; // UIでボタン登録
    private GamePadCommand _command;
    private int GetInputOB;
    private int selectedIndex = 0;

    private float inputCooldown = 0.25f; // 選択移動の受付間隔
    private float inputCooldownTimer = 0f;

    private bool confirmButtonReleased = true; // 決定ボタンが離された状態かどうか

    private void Start()
    {
        _command = new GamePadCommand();
        GetInputOB = (int)GameManager.Instance.inputDevice;

        if (menuButtons.Count > 0)
        {
            SelectButton(0); // 最初のボタンを選択状態に
        }
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
            EventSystem.current.SetSelectedGameObject(menuButtons[index].gameObject);
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
}