using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSize : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Vector3 _originalScale;
    [SerializeField] private Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1.2f);
    [SerializeField] private float _scaleSpeed = 10f; // �g��E�k���̑���

    private Vector3 _targetScale;
    private bool _isSelected = false;

    void Start()
    {
        _originalScale = transform.localScale;
        _targetScale = _originalScale;
    }

    void Update()
    {
        // ���݂̃X�P�[������I�����X�P�[���֊��炩�Ɉړ�
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.deltaTime * _scaleSpeed);
    }

    // �I�����̃X�P�[��
    public void OnSelect(BaseEventData eventData)
    {
        _isSelected = true;
        _targetScale = _selectedScale;
    }

    // �I���O�̃X�P�[��
    public void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
        _targetScale = _originalScale;
    }
}
