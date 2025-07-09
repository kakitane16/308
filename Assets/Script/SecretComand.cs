using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SecretComand : MonoBehaviour
{
    // 削除済みフラグ（連続実行防止）
    private bool alreadyDeleted = false;
    private bool alreadySwitched = false;

    void Update()
    {
        // --- キーボード: 3 + 8 + 0 同時押し ---
        bool isKeyboardCombo = Input.GetKey(KeyCode.Alpha3) &&
                               Input.GetKey(KeyCode.Alpha8) &&
                               Input.GetKey(KeyCode.Alpha0);

        // --- キーボード: K + P + T 同時押し ---
        bool isKeyboardComboInput = Input.GetKey(KeyCode.K) &&
                               Input.GetKey(KeyCode.P) &&
                               Input.GetKey(KeyCode.T);

        // --- ゲームパッド: Start + R2 + L2 同時押し ---
        bool isGamepadCombo = Gamepad.current != null &&
                              Gamepad.current.startButton.isPressed &&
                              Gamepad.current.rightTrigger.ReadValue() > 0.5f && // ★ R2
                              Gamepad.current.leftTrigger.ReadValue() > 0.5f;    // L2

        // --- ゲームパッド: L1 + R1 + Start 同時押し ---
        bool isGamepadComboInput = Gamepad.current != null &&
                              Gamepad.current.leftShoulder.isPressed &&
                              Gamepad.current.rightShoulder.isPressed &&
                              Gamepad.current.startButton.isPressed;


        //セーブの消去をするためのsecretCommand
        if (!alreadyDeleted && (isKeyboardCombo || isGamepadCombo))
        {
            GameManager.Instance.DeleteClearState();
            alreadyDeleted = true;
        }

        // 全キー/ボタンが離されたらフラグをリセット
        if (!Input.GetKey(KeyCode.Alpha3) &&
            !Input.GetKey(KeyCode.Alpha8) &&
            !Input.GetKey(KeyCode.Alpha0) &&
            (Gamepad.current == null ||
             (!Gamepad.current.startButton.isPressed &&
              Gamepad.current.rightTrigger.ReadValue() < 0.1f &&
              Gamepad.current.leftTrigger.ReadValue() < 0.1f)))
        {
            alreadyDeleted = false;
        }


        //Inputデバイス変更のためのsecretCommand
        if (!alreadySwitched && (isKeyboardComboInput || isGamepadComboInput))
        {
            if (isKeyboardComboInput)
            {
                GameManager.Instance.inputDevice = InputObject.GamePad;
                Debug.Log("InputDevice changed to: GamePad");
            }
            else if (isGamepadComboInput)
            {
                GameManager.Instance.inputDevice = InputObject.KeyBoad;
                Debug.Log("InputDevice changed to: Keyboard");
            }

            alreadySwitched = true;
        }

        // キー／ボタンをすべて離したらフラグをリセット（再変更可能に）
        if (!Input.GetKey(KeyCode.K) &&
            !Input.GetKey(KeyCode.P) &&
            !Input.GetKey(KeyCode.T) &&
            (Gamepad.current == null ||
             (!Gamepad.current.leftShoulder.isPressed &&
              !Gamepad.current.rightShoulder.isPressed &&
              !Gamepad.current.startButton.isPressed)))
        {
            alreadySwitched = false;
        }
    }
}
