using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObjectUI : MonoBehaviour, IClickable
{
    [SerializeField] protected bool _isVisual = true;

    public UnityEvent OnClickEvent;
    public UnityEvent OnEnterEvent;
    public UnityEvent OnExitEvent;
    public UnityEvent OnReleaseEvent;
    
    public void Enter()
    {
        OnEnterEvent?.Invoke();
    }

    public void Click()
    {
        OnClickEvent?.Invoke();
    }

    public void Release()
    {
        OnReleaseEvent?.Invoke();
    }

    public void Exit()
    {
        OnExitEvent?.Invoke();
    }
}