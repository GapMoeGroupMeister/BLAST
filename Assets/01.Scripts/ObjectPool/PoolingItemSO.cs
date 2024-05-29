using System;
using UnityEngine;

namespace ObjectPooling
{
    [CreateAssetMenu(menuName = "SO/Pool/PoolItem")]
    public class PoolingItemSO : ScriptableObject
    {
        public string enumName;
        public string poolingName;
        public string description;
        public int poolCount;
        public PoolableMono prefab;

        private void OnValidate()
        {
            if (prefab != null)
            {
                if (enumName != prefab.type.ToString())
                {
                    prefab = null;
                    Debug.LogWarning("Type missmatch!");
                }
            }
        }
    }
}
