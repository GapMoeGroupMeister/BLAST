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
             Debug.Log(DropItem(PoolType.Item_Skill, Vector3.zero));
        }

        public Item DropItem(PoolType type, Vector3 position)
        {
            var item = gameObject.Pop(type, this.transform) as WeaponItem;
            return item;
        }
    }
}