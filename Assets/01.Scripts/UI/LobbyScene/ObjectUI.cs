using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectUI : MonoBehaviour, IClickable
{
    public event Action OnClickEvent;
    public event Action OnEnterEvent;
    public event Action OnExitEvent;
    public event Action OnReleaseEvent;
    
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