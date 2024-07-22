using System;
using DG.Tweening;
using UnityEngine;

public class ButtonObject : ObjectUI
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _hoverColor;
    private Color _defaultColor;
    private void Awake()
    {
        _defaultColor = _spriteRenderer.color;
        
        OnEnterEvent.AddListener(HandleHoverEnter);
        OnExitEvent.AddListener(HandleHoverExit);
    }

    public void HandleHoverEnter()
    {
        _spriteRenderer.DOColor(_hoverColor, 0.1f);
    }
    
    public void HandleHoverExit()
    {
        _spriteRenderer.DOColor(_defaultColor, 0.1f);
    }
}