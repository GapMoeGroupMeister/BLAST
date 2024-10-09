using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    private Enemy _owner;
    [SerializeField]
    private Transform _laserTrm;
    [SerializeField]
    private float _length;
    [SerializeField]
    private LayerMask _whatIsWall;
    private bool _isEnable = false;

    private void Update()
    {
        if (_isEnable)
        { 
            Physics.Raycast(transform.position, transform.up, out RaycastHit hit, _length * 8, _whatIsWall);
            if(hit.collider != null )
            {
                _laserTrm.localScale = new Vector3(1, hit.distance / 2, 1);
                _laserTrm.localPosition = Vector3.up * hit.distance / 2;

            }
            else
            {
                _laserTrm.localScale = new Vector3(1, _length, 1);
                _laserTrm.localPosition = Vector3.up * _length;
            }
        }
    }

    public void EnableLaser(float length)
    {
        _isEnable = true;
        _length = length / 8;
        _laserTrm.gameObject.SetActive(true);
        _laserTrm.localScale = new Vector3(1, _length, 1);
        _laserTrm.localPosition = Vector3.up * _length;
    }

    public void DisableLaser()
    {
        _isEnable = false;
        _laserTrm.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(10);
        }
    }
}
