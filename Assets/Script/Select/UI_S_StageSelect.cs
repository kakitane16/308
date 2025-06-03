using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���p�b�h�܂��̓L�[�{�[�h�Ń^�C�g����ʂ̃{�^���I�����s���X�N���v�g�B
/// UI�^�C�g���̃{�^�����㉺�Ɉړ����āA����{�^���őI�𒆂̃{�^���̏��������s���܂��B
/// </summary>


public class UI_S_StageSelect : MonoBehaviour
{
    public List<Button> menuButtons; // UI�Ń{�^���o�^
    private GamePadCommand _command;
    private int GetInputOB;
    private int selectedIndex = 0;

    private float inputCooldown = 0.25f; // �I���ړ��̎�t�Ԋu
    private float inputCooldownTimer = 0f;

    private bool confirmButtonReleased = true; // ����{�^���������ꂽ��Ԃ��ǂ���

    private void Start()
    {
        _command = new GamePadCommand();
        GetInputOB = (int)GameManager.Instance.inputDevice;

        if (menuButtons.Count > 0)
        {
            SelectButton(0); // �ŏ��̃{�^����I����Ԃ�
        }
    }

    private void Update()
    {
        // �N�[���_�E�����͓��͂��󂯕t���Ȃ��悤��
        if (inputCooldownTimer > 0f)
        {
            inputCooldownTimer -= Time.deltaTime;
            return;
        }

        // �c�����̓��͂��擾
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
    // ====== �w��C���f�b�N�X�̃{�^����I����Ԃ� ======
    private void SelectButton(int index)
    {
        if (menuButtons != null && menuButtons.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(menuButtons[index].gameObject);
        }
    }
    // ====== ����{�^���������ꂽ�Ƃ��ɁA���ݑI�𒆂̃{�^�����s ======
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