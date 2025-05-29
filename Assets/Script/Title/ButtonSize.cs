using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSize : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Vector3 _originalScale;
    [SerializeField] private Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1.2f);
    [SerializeField] private float _scaleSpeed = 10f; // 拡大・縮小の速さ

    private Vector3 _targetScale;
    private bool _isSelected = false;

    void Start()
    {
        _originalScale = transform.localScale;
        _targetScale = _originalScale;
    }

    void Update()
    {
        // 現在のスケールから選択時スケールへ滑らかに移動
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.deltaTime * _scaleSpeed);
    }

    // 選択時のスケール
    public void OnSelect(BaseEventData eventData)
    {
        _isSelected = true;
        _targetScale = _selectedScale;
    }

    // 選択外のスケール
    public void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
        _targetScale = _originalScale;
    }
}
