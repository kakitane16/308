//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;
//using UnityEngine.SceneManagement;

///// <summary>
///// �Q�[���p�b�h�܂��̓L�[�{�[�h�Ń^�C�g����ʂ̃{�^���I�����s���X�N���v�g�B
///// UI�^�C�g���̃{�^�����㉺�Ɉړ����āA����{�^���őI�𒆂̃{�^���̏��������s���܂��B
///// </summary>


//public class UI_S_StageSelect : MonoBehaviour
//{
//    public List<Button> menuButtons; // UI�Ń{�^���o�^
//    private GamePadCommand _command;
//    private int GetInputOB;
//    private int selectedIndex = 0;

//    private float inputCooldown = 0.25f; // �I���ړ��̎�t�Ԋu
//    private float inputCooldownTimer = 0f;

//    private bool confirmButtonReleased = true; // ����{�^���������ꂽ��Ԃ��ǂ���

//    //=============================================================================================
//    public RectTransform content;       // ������UI�iPanel_Content�j
//    public float pageWidth = 800f;      // 1�y�[�W�̕�
//    public float slideSpeed = 10f;      // �ړ����x
//    private Vector2 targetPosition;     // �ړ���̈ʒu
//    private int currentPage = 0;        // ���݂̃y�[�W

//    public int maxPage = 2; // �y�[�W�� - 1�i��F3�y�[�W�Ȃ�2�j
//    //=============================================================================================


//    private void Start()
//    {
//        //=============================================================================================

//        targetPosition = content.anchoredPosition;
//        //=============================================================================================


//        _command = new GamePadCommand();
//        GetInputOB = (int)GameManager.Instance.inputDevice;

//        if (menuButtons.Count > 0)
//        {
//            SelectButton(0); // �ŏ��̃{�^����I����Ԃ�
//        }
//    }

//    private void Update()
//    {
//        //UI�ړ�========================================================================================================
//        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, Time.deltaTime * slideSpeed);
//        //======================================================================================================================


//        // �N�[���_�E�����͓��͂��󂯕t���Ȃ��悤��
//        if (inputCooldownTimer > 0f)
//        {
//            inputCooldownTimer -= Time.deltaTime;
//            return;
//        }

//        // �c�����̓��͂��擾
//        float vertical = _command.GetVerticalAxis(GetInputOB);

//        if (_command.LeftAction(GetInputOB))
//        {
//            inputCooldownTimer = inputCooldown;
//            selectedIndex = (selectedIndex + (-1) + menuButtons.Count) % menuButtons.Count;
//            SelectButton(selectedIndex);
//        }
//        if (_command.RightAction(GetInputOB))
//        {
//            inputCooldownTimer = inputCooldown;
//            selectedIndex = (selectedIndex + (1) + menuButtons.Count) % menuButtons.Count;
//            SelectButton(selectedIndex);
//        }

//        HandleDecision();
//    }
//    // ====== �w��C���f�b�N�X�̃{�^����I����Ԃ� ======
//    private void SelectButton(int index)
//    {
//        if (menuButtons != null && menuButtons.Count > 0)
//        {
//            EventSystem.current.SetSelectedGameObject(menuButtons[index].gameObject);
//        }
//    }
//    // ====== ����{�^���������ꂽ�Ƃ��ɁA���ݑI�𒆂̃{�^�����s ======
//    private void HandleDecision()
//    {
//        if (!_command.IsBbutton(GetInputOB))
//        {
//            confirmButtonReleased = true;
//        }

//        if (_command.IsBbutton(GetInputOB) && confirmButtonReleased)
//        {
//            confirmButtonReleased = false;
//            menuButtons[selectedIndex].onClick.Invoke();
//        }
//    }

//    //=============================================================================================

//    public void SlideLeft()
//    {
//        if (currentPage > 0)
//        {
//            currentPage--;
//            targetPosition = new Vector2(-pageWidth * currentPage, 0);
//        }
//    }
//    public void SlideRight()
//    {
//        if (currentPage < maxPage)
//        {
//            currentPage++;
//            targetPosition = new Vector2(-pageWidth * currentPage, 0);
//        }
//    }
//        //=============================================================================================

//}