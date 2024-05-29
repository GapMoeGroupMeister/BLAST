using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class PoolingManager : MonoSingleton<PoolingManager>
{
    private Dictionary<PoolingType, Pool<PoolableMono>> _pools
        = new Dictionary<PoolingType, Pool<PoolableMono>>();

    public PoolingTableSO listSO;

    private void Awake()
    {
        foreach (PoolingItemSO item in listSO.datas)
        {
            CreatePool(item);
        }
    }

    private void CreatePool(PoolingItemSO item)
    {
        var pool = new Pool<PoolableMono>(item.prefab, item.prefab.type, transform, item.poolCount);
        _pools.Add(item.prefab.type, pool);
    }

    public PoolableMono Pop(PoolingType type)
    {
        if (_pools.ContainsKey(type) == false)
        {
            Debug.LogError($"Prefab dose not exist on Pool : {type}");
            return null;
        }
        
        PoolableMono item = _pools[type].Pop();
        item.ResetItem();
        return item;
    }

    public void Push(PoolableMono obj, bool resetParent = false)
    {
        if (resetParent)
            obj.transform.SetParent(transform);
        _pools[obj.type].Push(obj);
    }
}
