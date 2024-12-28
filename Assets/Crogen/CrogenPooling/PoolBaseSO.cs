using System.Collections.Generic;
using UnityEngine;
using Crogen.CrogenPooling;

[System.Serializable]
public class PoolPair
{
    public string poolType;
    public GameObject prefab;
    public int poolCount;
}

public class PoolBaseSO : ScriptableObject
{
    public List<PoolPair> pairs;

    private void OnValidate()
    {
        PairInit();
    }

    public void PairInit()
    {
        if (pairs == null) return;
        foreach (var pair in pairs)
        {
            if (pair.prefab == null)
                return;

            if (!pair.prefab.TryGetComponent(out IPoolingObject poolingObject))
			{
                Debug.LogError("Script Has to \"IPoolingObject\"");
                return;
			}

            if (pair.poolType.Equals(string.Empty) && pair.prefab != null)
            {
                pair.poolType = pair.prefab.name;
            }
        }
    }
}
