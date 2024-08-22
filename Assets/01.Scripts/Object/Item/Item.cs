using System;
using Crogen.ObjectPooling;
using UnityEngine;

namespace ItemManage
{
    public abstract class Item : MonoBehaviour, IInteractable, IDetectable, IPoolingObject
    {
        [field:SerializeField] public ItemType ItemType { get; set; }
        public PoolType OriginPoolType { get; set; }
        GameObject IPoolingObject.gameObject { get; set; }
        public event Action OnInteractEvent;
        public event Action OnDetectEvent;
        public event Action OnUnDetectEvent;

        [ContextMenu("Interact")]
        public virtual void Interact()
        {
            OnInteractEvent?.Invoke();
            this.Push();
        }

        [ContextMenu("Detect")]
        public virtual void Detect()
        {
            OnDetectEvent?.Invoke();
        }

        [ContextMenu("UnDetect")]
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
}
