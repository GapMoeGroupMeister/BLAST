using System.Collections.Generic;
using Crogen.CrogenPooling;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SuppliesDropItemSO")]
public class SuppliesDropItemSO : ScriptableObject
{
    public List<SuppliesDropItem> suppliesDropItemSOList;
}

[System.Serializable]
public struct SuppliesDropItem
{
    public ItemPoolType poolType;
    public float dropRate;
}
