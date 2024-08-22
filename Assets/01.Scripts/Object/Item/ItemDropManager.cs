using System;
using System.Collections.Generic;
using Crogen.ObjectPooling;
using UnityEngine;

namespace ItemManage
{
    public class ItemDropManager : MonoSingleton<ItemDropManager>
    {

        private void Start()
        {
        }

        public Item DropItem(PoolType type, Vector3 position)
        {
            SpeedItem item = gameObject.Pop(type, this.transform) as SpeedItem;
            return item;
        }
    }
}