using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenPooling
{
    public static class PopCore
    {
        private static PoolManager _poolManager { get; set; }
        private static List<PoolBaseSO> _poolBaseList;
        
        public static void Init(PoolManager poolManager, List<PoolBaseSO> poolBaseList)
        {
            _poolManager = poolManager;
            _poolBaseList = poolBaseList;
        }

        public static IPoolingObject Pop(this GameObject target, Enum type, Transform parent)
        {
            string typeName = type.ToString();

            try
            {
                IPoolingObject poolingObject;

                if (PoolManager.poolDic[typeName].Count == 0)
                {
					foreach (var poolBase in _poolBaseList)
					{
                        for (int i = 0; i < poolBase.pairs.Count; i++)
                        {
                            if (poolBase.pairs[i].poolType.Equals(typeName))
                            {
                                poolingObject = PoolManager.CreateObject(poolBase.pairs[i], Vector3.zero, Quaternion.identity);
                                PoolManager.PoolingObjectInit(poolingObject, typeName, PoolManager.Transform);
                                break;
                            }
                        }
                    }
                }
                poolingObject = PoolManager.poolDic[typeName].Pop();
                GameObject obj = poolingObject.gameObject;

                obj.SetActive(true);

                obj.transform.SetParent(parent);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                poolingObject.OnPop();

                return poolingObject;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError("You should make 'PoolManager'!");
                throw;
            }
        }

        public static IPoolingObject Pop(this GameObject target, Enum type, Vector3 pos, Quaternion rot)
        {
            string typeName = type.ToString();

            try
            {
                IPoolingObject poolingObject;

                if (PoolManager.poolDic[typeName].Count == 0)
                {
					foreach (var poolBase in _poolBaseList)
					{
                        for (int i = 0; i < poolBase.pairs.Count; i++)
                        {
                            if (poolBase.pairs[i].poolType.Equals(typeName))
                            {
                                poolingObject = PoolManager.CreateObject(poolBase.pairs[i], Vector3.zero, Quaternion.identity);
                                PoolManager.PoolingObjectInit(poolingObject, typeName, PoolManager.Transform);
                                break;
                            }
                        }
                    }
                }
                poolingObject = PoolManager.poolDic[typeName].Pop();
                GameObject obj = poolingObject.gameObject;

                obj.SetActive(true);

                obj.transform.SetParent(null);
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                poolingObject.OnPop();

                return poolingObject;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError("You should make 'PoolManager'!");
                throw;
            }
        }
    }
}