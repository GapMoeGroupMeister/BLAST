using System;
using Crogen.ObjectPooling;
using UnityEngine;

namespace ItemManage
{
    public abstract class Item : MonoBehaviour, IPoolingObject
    {
        [field:SerializeField] public ItemType ItemType { get; set; }
        public PoolType OriginPoolType { get; set; }
        GameObject IPoolingObject.gameObject { get; set; }
        public event Action OnInteractEvent;
        
        protected Player _player;

        [ContextMenu("Interact")]
        public virtual void Interact()
        {
            OnInteractEvent?.Invoke();
            this.Push();
        }


        public virtual void OnPop()
        {
        }

        public virtual void OnPush()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _player = player;
                Interact();
            }
        }
    }
}
