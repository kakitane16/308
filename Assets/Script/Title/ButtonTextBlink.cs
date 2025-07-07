using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTextBlink : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Graphic targetText;

    private Color _normalColor = Color.white;
    private Color _highlightColor = Color.black;

    void Start()
    {
        if (targetText == null)
        {
            targetText = GetComponentInChildren<Graphic>();
        }

        if (targetText != null)
        {
            targetText.color = _normalColor;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (targetText != null)
        {
            targetText.color = _highlightColor;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (targetText != null)
        {
            targetText.color = _normalColor;
        }
    }
}