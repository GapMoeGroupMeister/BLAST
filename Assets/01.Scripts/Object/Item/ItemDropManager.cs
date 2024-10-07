using System.Collections.Generic;
using System.Text;
using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

namespace ItemManage
{
    public class ItemDropManager : MonoSingleton<ItemDropManager>
    {
        [SerializeField] private Supplies _supplyPrefab;
        [SerializeField] private SupplyAlertPanel _alertPanel;
        public List<SupplyDropPoint> dropPositions;

        public void SendSupply(WaveSO waveSO)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < waveSO.supplyAmount; i++)
            {
                if (i != 0)
                { // 처음엔 제외
                    stringBuilder.Append(", ");
                }
                SupplyDropPoint point = dropPositions[Random.Range(0, dropPositions.Count)];
                stringBuilder.Append(point.pointName);
                Vector3 randomPosition = point.position;
                Supplies supply = Instantiate(_supplyPrefab);
                supply.Initialize(waveSO.dropItemAmount);
                supply.SendSupply(randomPosition, 1.3f);
            }
            _alertPanel.ShowAlert(stringBuilder.ToString());
        }

        
        public Item DropItem(PoolType type)
        {
            var item = gameObject.Pop(type, transform) as Item;
            item.transform.position = transform.position;
            return item;
        }

        public Item DropItem(PoolType type, Vector3 startPos)
        {
            var item = gameObject.Pop(type, transform) as Item;
            item.transform.position = startPos;
            item.transform.DOJump(item.GetRandomPosition(10f), 1, 1, 1);
            Debug.Log("DropItem");
            return item;
        }
    }
}