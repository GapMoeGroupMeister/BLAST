using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MenuButtonGroup : MonoBehaviour, IWindowPanel
{
    [SerializeField] private RectTransform[] _buttons;
    
    [Header("Buttons Active Setting")]
    [SerializeField] private float _defaultButtonXPos;
    [SerializeField] private float _activeButtonXPos;
    [SerializeField] private float _activeDuration = 0.3f;
    [SerializeField] private float _activeTerm = 0.05f;
    
    
    public void Open()
    {
        StartCoroutine(ButtonActiveCoroutine(true));
    }

    public void Close()
    {
        StartCoroutine(ButtonActiveCoroutine(false));

    }
    
    private IEnumerator ButtonActiveCoroutine(bool isActive)
    {
        float targetValue = isActive ? _activeButtonXPos : _defaultButtonXPos;
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].DOAnchorPosX(targetValue, _activeDuration);
            yield return new WaitForSeconds(_activeTerm);
        }
    }
}
