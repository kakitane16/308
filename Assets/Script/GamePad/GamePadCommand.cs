using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//������V�Ԃ��͉̂����擾���邽�߂̈����Ƃ��Ďg��
public enum InputObject
{
    None = 0,
    GamePad,
    KeyBoad,
}
public class GamePadCommand : MonoBehaviour
{
    //�߂�ǂ����������̂ł����\���܂���
    private Vector2 deadzone = new (0.3f,-0.3f);

    //�Q�[���p�b�h���L�[�{�[�h����ǂ�����g���Ă邩���肷��v���O�����ł�
    //���t�g�X�e�B�b�N��������ɓ����Ă���Ƃ��@�L�[�{�[�h�Ȃ炗�L�[
    public bool UpAction(int ipt)
    {
        
        switch (ipt)
        {
        case (int)InputObject.GamePad:
            if (Gamepad.current != null)
            {
                if (Gamepad.current.leftStick.value.y > deadzone.x)
                {
                    return true;
                }
            }
            break;
        case (int)InputObject.KeyBoad:
            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed)
                {
                    return true;
                }
            }
            break;
        }
        return false;
    }

    //���t�g�X�e�B�b�N���������ɓ����Ă���Ƃ��@�L�[�{�[�h�Ȃ�S�L�[
    public bool DownAction(int ipt)
    {
        switch (ipt)
        {
            case (int)InputObject.GamePad:
                if (Gamepad.current != null)
                {
                    if (Gamepad.current.leftStick.value.y < deadzone.y)
                    {
                        return true;
                    }
                }
                break;
            case (int)InputObject.KeyBoad:
                if (Keyboard.current != null)
                {
                    if (Keyboard.current.sKey.isPressed)
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
    }

    public bool LeftAction(int ipt)
    {
        switch (ipt)
        {
            case (int)InputObject.GamePad:
                if (Gamepad.current != null)
                {
                    if (Gamepad.current.leftStick.value.x < deadzone.y)
                    {
                        return true;
                    }
                }
                break;
            case (int)InputObject.KeyBoad:
                if (Keyboard.current != null)
                {
                    if (Keyboard.current.sKey.isPressed)
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
    }
    public bool RightAction(int ipt)
    {
        switch (ipt)
        {
            case (int)InputObject.GamePad:
                if (Gamepad.current != null)
                {
                    if (Gamepad.current.leftStick.value.x > deadzone.x)
                    {
                        return true;
                    }
                }
                break;
            case (int)InputObject.KeyBoad:
                if (Keyboard.current != null)
                {
                    if (Keyboard.current.sKey.isPressed)
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
    }

    //B�{�^����������Ă��鎞�L�[�{�[�h�Ȃ�space
    public bool IsBbutton(int ipt)
    {
        switch (ipt)
        {
            case (int)InputObject.GamePad:
                if (Gamepad.current != null)
                {
                    if (Gamepad.current.bButton.isPressed)
                    {
                        return true;
                    }
                }
                break;
            case (int)InputObject.KeyBoad:
                if (Keyboard.current != null)
                {
                    if (Keyboard.current.spaceKey.isPressed)
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
    }

    //B�{�^���������ꂽ���@�L�[�{�[�h�Ȃ�space
    public bool WasBbutton(int ipt)
    {
        switch (ipt)
        {
            case (int)InputObject.GamePad:
                if (Gamepad.current != null)
                {
                    if (Gamepad.current.bButton.wasReleasedThisFrame)
                    {
                        return true;
                    }
                }
                break;
            case (int)InputObject.KeyBoad:
                if (Keyboard.current != null)
                {
                    if (Keyboard.current.spaceKey.wasReleasedThisFrame)
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
    }
   
    public float GetVerticalAxis(int ipt)
    {
        switch (ipt)
        {
            case (int)InputObject.GamePad:
                if (Gamepad.current != null)
                {
                    return Gamepad.current.leftStick.ReadValue().y;
                }
                break;
            case (int)InputObject.KeyBoad:
                if (Keyboard.current != null)
                {
                    float value = 0f;
                    if (Keyboard.current.wKey.isPressed) value += 1f;
                    if (Keyboard.current.sKey.isPressed) value -= 1f;
                    return value;
                }
                break;
        }
        return 0f;
    }

}

