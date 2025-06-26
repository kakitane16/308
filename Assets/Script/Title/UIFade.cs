using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIFade : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float fadeSpeed = 2f;

    private Material _mat;
    private float _fade = 0f;
    private bool _selected = false;

    void Start()
    {
        if (targetImage != null)
        {
            _mat = Instantiate(targetImage.material); // •¡»‚µ‚Ä‘¼UI‚Æ•ª—£
            targetImage.material = _mat;
        }
    }

    void Update()
    {
        if (_mat == null) return;

        _fade = Mathf.MoveTowards(_fade, _selected ? 1f : 0f, Time.deltaTime * fadeSpeed);
        _mat.SetFloat("_Fade", _fade);
    }

    public void OnSelect(BaseEventData eventData) => _selected = true;
    public void OnDeselect(BaseEventData eventData) => _selected = false;
}
