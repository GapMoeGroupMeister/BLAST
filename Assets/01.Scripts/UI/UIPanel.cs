using DG.Tweening;
using UnityEngine;

public class UIPanel : MonoBehaviour, IWindowPanel
{
    protected CanvasGroup _canvasGroup;
    [SerializeField] protected bool _useUnscaledTime;
    [SerializeField] protected float _activeDuration = 1f;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Open()
    {
        SetCanvasActive(true);
    }

    public virtual void Close()
    {
        SetCanvasActive(false);
    }

    public void SetCanvasActive(bool value)
    {
        _canvasGroup.DOFade(value ? 1f : 0f, _activeDuration).SetUpdate(_useUnscaledTime);
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }
}
