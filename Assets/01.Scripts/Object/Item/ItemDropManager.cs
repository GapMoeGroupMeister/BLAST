using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

namespace ItemManage
{
    public class ItemDropManager : MonoSingleton<ItemDropManager>
    {
        public Item DropItem(PoolType type)
        {
            var item = gameObject.Pop(type, transform) as Item;
            item.transform.position = transform.position;
            return item;
        }
        
        public Item DropItem(PoolType type, Vector3 startPos)
        {
            var item = gameObject.Pop(type, transform) as Item;
            item.transform.position = startPos;
            item.transform.DOJump(item.GetRandomPosition(10f), 1, 1, 1);
            Debug.Log("DropItem");
            return item;
        }
    }
}