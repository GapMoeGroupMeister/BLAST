using System;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable, IDetectable, IPoolingObject
{
    [field:SerializeField] public ItemType ItemType { get; set; }
    public PoolType OriginPoolType { get; set; }
    public GameObject gameObject { get; set; }
    public event Action OnInteractEvent;
    public event Action OnDetectEvent;
    public event Action OnUnDetectEvent;
    
    public virtual void Interact()
    {
        OnInteractEvent?.Invoke();
    }

    public virtual void Detect()
    {
        OnDetectEvent?.Invoke();
    }

    public virtual void UnDetect()
    {
        OnUnDetectEvent?.Invoke();
    }

    
    public virtual void OnPop()
    {
        
    }

    public virtual void OnPush()
    {
        
    }
}