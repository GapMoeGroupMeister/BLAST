using UnityEngine;

namespace Crogen.CrogenPooling
{
    public static class PushCore
    {
        private static PoolManager _poolManager { get; set; }

        public static void Init(PoolManager poolManager)
        {
            _poolManager = poolManager;
        }
        
        public static void Push(this IPoolingObject target)
        {
            target.OnPush();
            target.gameObject.transform.SetParent(_poolManager.transform);
            target.gameObject.transform.gameObject.SetActive(false);
            PoolManager.poolDic[target.OriginPoolType].Push(target);
        }
    }
}