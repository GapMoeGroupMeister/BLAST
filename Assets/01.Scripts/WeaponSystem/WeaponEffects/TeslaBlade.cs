using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaBlade : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage = 1;
    private ParticleSystem _parti;
    private Collider[] _enemies;

    private void Awake()
    {
        _enemies = new Collider[4];
    }

    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        int cnt = Physics.OverlapSphereNonAlloc(transform.position, _attackRange, _enemies, _whatIsEnemy);

        for (int i = 0; i < cnt; ++i)
        {
            if (_enemies[i].TryGetComponent(out IDamageable entity))
            {
                Debug.Log(entity);
                entity.TakeDamage(_damage);
            }
        }

        yield return new WaitForSeconds(1f);

        Debug.Log(cnt);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
