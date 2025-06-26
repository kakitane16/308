using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 選択されたボタンを大きさを変えるスクリプト。
/// </summary>
public class ButtonSize : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Vector3 _originalScale; //元のサイズ
    [SerializeField] private Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1.2f); // 選択後のサイズ(インスペクターでサイズを変えれるように)
    [SerializeField] private float _scaleSpeed = 10f; // 大きさが変わる速度(滑らかに変わるように)
    [SerializeField] private float _pulse = 0.05f; // 拡縮の振幅
    [SerializeField] private float _pulseSpeed = 4f;     // 拡縮の速さ
    // ここにシーン名を入れる
    //[SerializeField] private string sceneToLoad;

    private Vector3 _targetScale; // 選択対象
    private bool _isSelected = false; //選択中かどうか
    private float _pulseTimer = 0f;

    public bool IsSelected => _isSelected;  // 外部から選択状態を取得可能に

    void Start()
    {
        _originalScale = transform.localScale;
        _targetScale = _originalScale;
    }

    // =======選択されたら拡縮で大きさを変える========
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
    // ==========選択中==========
    public void OnSelect(BaseEventData eventData)
    {
        _isSelected = true;
        _pulseTimer = 0f; // リセットして綺麗に脈動開始
    }
    // ==========選択外==========
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
