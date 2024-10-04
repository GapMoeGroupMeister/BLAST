using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactHit : MonoBehaviour
{
    private bool _isActive = false;
    public int damage;

    public void SetActive(bool isActive)
    { _isActive = isActive; }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;
        if(other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }
    }
}
