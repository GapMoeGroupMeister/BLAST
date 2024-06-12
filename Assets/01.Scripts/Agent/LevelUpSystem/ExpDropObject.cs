using System.Collections;
using ObjectPooling;
using UnityEngine;

public class ExpDropObject : PoolableMono
{
    [SerializeField] private GameObject Player;
    [SerializeField] private int _exp;
    public int Exp => _exp;

    public void Follow()
    {
        StartCoroutine(PlayerFollow());
    }

    private IEnumerator PlayerFollow()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            transform.position = 
                Vector3.MoveTowards(transform.position, 
                    Player.transform.position, 10 * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LevelUpSystem levelUpSystem))
        {
            levelUpSystem.AddExp(_exp);
            //PoolingManager.Instance.Push(PoolingType.Exp, this);
        }
    }

    public override void ResetItem()
    {
        StopAllCoroutines();
    }
}