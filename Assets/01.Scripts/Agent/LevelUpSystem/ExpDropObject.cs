using System.Collections;
using UnityEngine;
using Crogen.CrogenPooling;

public class ExpDropObject : MonoBehaviour, IPoolingObject
{
    [SerializeField] private GameObject Player;
    [SerializeField] private int _exp;
    public int Exp => _exp;

    public string OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }

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


    public void OnPop()
    {
    }

    public void OnPush()
    {
        StopAllCoroutines();
    }
}