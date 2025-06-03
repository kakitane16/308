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

    // �����ɃV�[����������
    //[SerializeField] private string sceneToLoad;

    private Vector3 _targetScale; // �I��Ώ�
    private bool _isSelected = false; //�I�𒆂��ǂ���

    public bool IsSelected => _isSelected;  // �O������I����Ԃ��擾�\��

    void Start()
    {
        _originalScale = transform.localScale;
        _targetScale = _originalScale;
    }

    // =======�I�����ꂽ�珙�X�ɑ傫����ς���========
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.deltaTime * _scaleSpeed);
    }
    // ==========�I��==========
    public void OnSelect(BaseEventData eventData)
    {
        _isSelected = true;
        _targetScale = _selectedScale;
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
