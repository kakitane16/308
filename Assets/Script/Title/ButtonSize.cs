using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSize : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Vector3 _originalScale;
    [SerializeField] private Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1.2f);
    [SerializeField] private float _scaleSpeed = 10f;

    // ここにシーン名を入れる
    [SerializeField] private string sceneToLoad;

    private Vector3 _targetScale;
    private bool _isSelected = false;

    public bool IsSelected => _isSelected;  // 外部から選択状態を取得可能に

    void Start()
    {
        _originalScale = transform.localScale;
        _targetScale = _originalScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.deltaTime * _scaleSpeed);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _isSelected = true;
        _targetScale = _selectedScale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
        _targetScale = _originalScale;
    }

    public string GetSceneName()
    {
        return sceneToLoad;
    }
}
