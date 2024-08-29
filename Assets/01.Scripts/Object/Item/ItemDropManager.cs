using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

namespace ItemManage
{
    public class ItemDropManager : MonoSingleton<ItemDropManager>
    {

        private void Start()
        {
            Debug.Log(DropItem(PoolType.Item_Speed, new Vector3(10,0)));
        }

        public Item DropItem(PoolType type, Vector3 position)
        {
            var item = gameObject.Pop(type, this.transform) as Item;
            item.transform.position = position;
            return item;
        }
        
        public Item DropItem(PoolType type)
        {
            var item = gameObject.Pop(type, this.transform) as Item;
            item.transform.DOJump(item.GetRandomPosition(10f), 1, 1, 1);
            return item;
        }
    }
}