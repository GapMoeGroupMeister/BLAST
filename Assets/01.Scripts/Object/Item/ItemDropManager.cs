using System.Collections.Generic;
using Crogen.ObjectPooling;
using UnityEngine;

public class ItemDropManager : MonoSingleton<ItemDropManager>
{
    [field:SerializeField] public List<ItemTableSO> ItemTableSO { get; set; }
    
    public Item DropItem(int id, int itemIndex, Vector3 position)
    {
        ItemSO itemSO = GetItemSO(id, itemIndex);
        // Pooling으로 아이템 가져오기
        return null;
    }
    
    private ItemTableSO GetItemTableSO(int id)
    {
        return ItemTableSO.Find(x => x.id == id);
    }
    
    public ItemSO GetItemSO(int id, int itemIndex)
    {
        return GetItemTableSO(id).items[itemIndex];
    }
}