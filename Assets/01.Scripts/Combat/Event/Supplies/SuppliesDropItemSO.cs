using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SuppliesDropItemSO")]
public class SuppliesDropItemSO : ScriptableObject
{
    public List<SuppliesDropItem> suppliesDropItemSOList;
}

[System.Serializable]
public struct SuppliesDropItem
{
    public PoolType poolType;
    public float dropRate;
}
