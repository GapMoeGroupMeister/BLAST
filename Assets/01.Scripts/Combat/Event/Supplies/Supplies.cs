using System.Collections;
using ItemManage;
using UnityEngine;
using Random = UnityEngine.Random;

public class Supplies : MonoBehaviour
{
    [SerializeField] private SuppliesDropItemSO _supplies;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _dropItemCount = 3;
    private bool _isDrop;

    private void OnEnable()
    {
        _isDrop = false;
    }
    
    [ContextMenu("Get Supplies")]
    public void GetSupplies()
    {
        GetSupplies(new Vector3(0, 100, 0), Vector3.zero, 10f);
    }

    public void GetSupplies(Vector3 startPos, Vector3 position, float speed)
    {
        _speed = speed;
        StartCoroutine(SuppliesEffect(startPos, position));
    }

    private IEnumerator SuppliesEffect(Vector3 startPos, Vector3 endPos)
    {
        transform.position = startPos;
        Vector3 direction = (endPos - startPos).normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        while (transform.position != endPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError(other.gameObject.name);
        if (_isDrop) return;
        Debug.Log("Get Supplies");
        for (int i = 0; i < _dropItemCount; i++)
        {
            DropSupplies();
        }
        _isDrop = true;
        Destroy(gameObject);
    }

    private void DropSupplies()
    {
        float max = 100f;
        float rate = 0f;
        var supplies = _supplies.suppliesDropItemSOList;
        for (int i = 0; i < supplies.Count; i++)
        {
            var dropRate = Random.Range(0, 100);
            rate += supplies[i].dropRate;
            if (dropRate <= rate)
            {
                var item = ItemDropManager.Instance.DropItem(supplies[i].poolType, transform.position);
                Debug.Log(item.name);
                break;
            }
        }
    }
}