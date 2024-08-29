using Crogen.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _centerTrm;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private List<PoolType> _enemyList;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            Spawn();
            yield return new WaitForSeconds(3f);
        }
    }

    private void Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        pos.Normalize();
        pos *= _radius;
        Enemy enemy = gameObject.Pop(_enemyList[Random.Range(0, _enemyList.Count)], transform) as Enemy;
        enemy.transform.position = pos;
    }
}
