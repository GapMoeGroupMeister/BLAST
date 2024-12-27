using System.Text;
using Crogen.CrogenPooling;
using DG.Tweening;
using UnityEngine;

namespace ItemManage
{
    public class ItemDropManager : MonoSingleton<ItemDropManager>
    {
        [SerializeField] private Supplies _supplyPrefab;
        [SerializeField] private SupplyAlertPanel _alertPanel;

        public void SendSupply(WaveSO waveSO)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < waveSO.supplyAmount; i++)
            {
                if (i != 0)
                { // 처음엔 제외
                    stringBuilder.Append(", ");
                }
                Transform player = GameManager.Instance.Player.transform;
                Vector3 point = player.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                Vector3 randomPosition = point;
                Supplies supply = Instantiate(_supplyPrefab);
                supply.Initialize(waveSO.dropItemAmount);
                supply.SendSupply(randomPosition, 1.3f);
            }
            _alertPanel.ShowAlert(stringBuilder.ToString());
        }

        public Item DropItem(ItemPoolType type, Vector3 startPos)
        {
            var item = gameObject.Pop(type, startPos, Quaternion.identity) as Item;
            item.transform.DOJump(item.GetRandomPosition(5f), 1, 1, 1);
            Debug.Log("DropItem");
            return item;
        }
    }
}