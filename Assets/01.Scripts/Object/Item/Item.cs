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
        
        protected Player _player;

        [ContextMenu("Interact")]
        public virtual void Interact()
        {
            GetEffect();
            this.Push();
        }
        
        protected abstract void GetEffect();


        public virtual void OnPop()
        {
            StartCoroutine(PushCoRoutine());
        }

        private IEnumerator PushCoRoutine()
        {
            yield return new WaitForSeconds(_itemEffectDuration);
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
            var randomPos = UnityEngine.Random.insideUnitSphere * radius + transform.position;
            randomPos.y = 0;
            return randomPos;
        }
    }
}
