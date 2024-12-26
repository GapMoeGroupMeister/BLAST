using DG.Tweening;
using ItemManage;
using UnityEngine;
using Random = UnityEngine.Random;

public class Supplies : MonoBehaviour, IDamageable
{
    [SerializeField] private SuppliesDropItemSO _supplies;
    [SerializeField] private int _dropItemCount = 3;
    [SerializeField] private FeedbackPlayer _flyFeedback;
    [SerializeField] private FeedbackPlayer _arriveFeedback;
    [SerializeField] private SupplyUI _supplyUIPf;
    private bool _isDrop;
    private bool _isDestroy;
    private float _speed = 1f;
    private readonly Vector3 _dropOffset = new Vector3(-70f, 200f, -70f);
    private SupplyUI _uiInstance;
    
    private void OnEnable()
    {
        _isDrop = false;
    }


    public void Initialize(int dropAmount)
    {
        _dropItemCount = dropAmount;
    }

    public void SendSupply(Vector3 targetPosition, float speed = 1f)
    {
        _speed = speed;
        _flyFeedback.PlayFeedback();
        Vector3 startPos = targetPosition + _dropOffset;
        transform.position = startPos;
        Vector3 direction = (targetPosition - startPos).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.DOMove(targetPosition, 2f / speed).SetEase(Ease.InExpo).OnComplete(() =>
        {
            _arriveFeedback.PlayFeedback();
            _flyFeedback.FinishFeedback();
        });
        _uiInstance = Instantiate(_supplyUIPf, UIManager.Instance.canvasTrm);
        _uiInstance.Init(transform);
    }


    private void DestroySupply()
    {
        if (_isDestroy) return;
        for (int i = 0; i < _dropItemCount; i++)
        {
            DropSupplies();
        }
        _isDestroy = true;
        Destroy(_uiInstance);
        Destroy(gameObject);

    }

    private void DropSupplies()
    {
        float rate = 0f;
        var supplies = _supplies.suppliesDropItemSOList;
        for (int i = 0; i < supplies.Count; i++)
        {
            var dropRate = Random.Range(0, 100);
            rate += supplies[i].dropRate;
            if (dropRate <= rate)
            {
                // var item = ItemDropManager.Instance.DropItem(supplies[i].poolType, transform.position);
                // Debug.Log(item.name);
                break;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        DestroySupply();
    }

    public void RestoreHealth(int amount)
    {
        // SOLID위반이긴 한데. 어쩔티비 어쩔냉장고
    }

    public void CheckDie()
    {
        // 니 또한
    }
}