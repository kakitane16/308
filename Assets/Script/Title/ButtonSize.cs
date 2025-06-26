using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �I�����ꂽ�{�^����傫����ς���X�N���v�g�B
/// </summary>
public class ButtonSize : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Vector3 _originalScale; //���̃T�C�Y
    [SerializeField] private Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1.2f); // �I����̃T�C�Y(�C���X�y�N�^�[�ŃT�C�Y��ς����悤��)
    [SerializeField] private float _scaleSpeed = 10f; // �傫�����ς�鑬�x(���炩�ɕς��悤��)
    [SerializeField] private float _pulse = 0.05f; // �g�k�̐U��
    [SerializeField] private float _pulseSpeed = 4f;     // �g�k�̑���
    // �����ɃV�[����������
    //[SerializeField] private string sceneToLoad;

    private Vector3 _targetScale; // �I��Ώ�
    private bool _isSelected = false; //�I�𒆂��ǂ���
    private float _pulseTimer = 0f;

    public bool IsSelected => _isSelected;  // �O������I����Ԃ��擾�\��

    void Start()
    {
        _originalScale = transform.localScale;
        _targetScale = _originalScale;
    }

    // =======�I�����ꂽ��g�k�ő傫����ς���========
    void Update()
    {
        if (_isSelected)
        {
            _pulseTimer += Time.deltaTime * _pulseSpeed;
            float scaleOffset = Mathf.Sin(_pulseTimer) * _pulse;
            Vector3 pulsatingScale = _selectedScale + Vector3.one * scaleOffset;
            transform.localScale = Vector3.Lerp(transform.localScale, pulsatingScale, Time.deltaTime * _scaleSpeed);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _originalScale, Time.deltaTime * _scaleSpeed);
        }
    }
    // ==========�I��==========
    public void OnSelect(BaseEventData eventData)
    {
        _isSelected = true;
        _pulseTimer = 0f; // ���Z�b�g�����Y��ɖ����J�n
    }
    // ==========�I���O==========
    public void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
        _targetScale = _originalScale;
    }

    //public string GetSceneName()
    //{
    //    return sceneToLoad;
    //}
}
