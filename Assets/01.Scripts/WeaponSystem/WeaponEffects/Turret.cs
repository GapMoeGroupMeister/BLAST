using System;
using System.Collections;
using Crogen.ObjectPooling;
using UnityEngine;

public class Turret : WeaponEffect, IPoolingObject
{
    [SerializeField] private Transform _turretHeadTrm;
    [SerializeField] private Transform _turretFireTrm;
    [SerializeField] private Transform _target;
    [SerializeField] private PoolType _poolType; 
    [SerializeField] private float _fireRate;
    [SerializeField] private float _dieTime;
    public PoolType OriginPoolType { get; set; }
    public GameObject gameObject { get; set; }
    public bool IsPushed { get; private set; }
    
    private float _fireRateTimer;

    private void Update()
    {
        _fireRateTimer += Time.deltaTime;
        if (_fireRateTimer >= _fireRate)
        {
            _fireRateTimer = 0;
            Shot();
        }
        if (_target != null)
        {
            
            RotateTurretHead(1f);
        }
    }

    public void SetTarget(Transform targetTrm)
    {
        _target = targetTrm;
    }
    
    public void RotateTurretHead(float rotationSpeed)
    {
        if (_target == null) return;
        Vector3 dir = _target.position - _turretHeadTrm.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(_turretHeadTrm.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        _turretHeadTrm.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shot()
    {
        gameObject.Pop(_poolType, _turretFireTrm.position, _turretHeadTrm.rotation);
    }

    
    public void OnPop()
    {
        IsPushed = false;
        StartCoroutine(AutoDie());
    }

    private IEnumerator AutoDie()
    {
        yield return new WaitForSeconds(_dieTime);
        this.Push();
    }

    public void OnPush()
    {
        IsPushed = true;
    }
}