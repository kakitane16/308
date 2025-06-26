using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonTextBlink : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Graphic textGraphic; // Text or TextMeshProUGUI (自動設定推奨)
    [SerializeField] private float blinkSpeed = 2f; // 点滅速度
    [SerializeField] private float minAlpha = 0.3f;
    [SerializeField] private float maxAlpha = 1f;

    private bool _isSelected = false;
    private float _timer = 0f;
    private Color _originalColor;

    void Start()
    {
        if (textGraphic == null)
        {
            // 子から自動でTextやTextMeshProUGUIを見つける（Graphicを継承）
            textGraphic = GetComponentInChildren<Graphic>();
        }

        if (textGraphic != null)
        {
            _originalColor = textGraphic.color;
        }
    }

    void Update()
    {
        if (_isSelected && textGraphic != null)
        {
            _timer += Time.deltaTime * blinkSpeed;
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(_timer) + 1f) / 2f);
            Color newColor = _originalColor;
            newColor.a = alpha;
            textGraphic.color = newColor;
        }
        else if (textGraphic != null)
        {
            // 選択解除時に透明度を戻す
            Color resetColor = _originalColor;
            resetColor.a = maxAlpha;
            textGraphic.color = Color.Lerp(textGraphic.color, resetColor, Time.deltaTime * 10f);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _isSelected = true;
        _timer = 0f;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
    }
}
