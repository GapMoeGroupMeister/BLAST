using System;
using System.Collections;
using Crogen.ObjectPooling;
using UnityEngine;

namespace ItemManage
{
    public abstract class Item : MonoBehaviour, IPoolingObject
    {
        [field:SerializeField] public ItemType ItemType { get; set; }
        [SerializeField] protected float _itemEffectDuration;
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
            StartCoroutine(PushCoRoutine());
        }

        private IEnumerator PushCoRoutine()
        {
            yield return new WaitForSeconds(1f);
            this.Push();
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
        
        public Vector3 GetRandomPosition(float radius)
        {
            var randomPos = UnityEngine.Random.insideUnitSphere * radius;
            randomPos.y = 0;
            return randomPos;
        }
    }
}
