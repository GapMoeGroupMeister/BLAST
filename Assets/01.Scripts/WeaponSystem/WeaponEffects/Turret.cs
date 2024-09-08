using System;
using Crogen.ObjectPooling;
using UnityEngine;

public class Turret : WeaponEffect, IPoolingObject
{
    [SerializeField] private Transform _turretHeadTrm;
    [SerializeField] private Transform _target;
    [SerializeField] private PoolType _poolType; 
    public PoolType OriginPoolType { get; set; }
    public GameObject gameObject { get; set; }
    

    private void Update()
    {
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

    public void Shot()
    {
        Bullet b = gameObject.Pop(_poolType, _turretHeadTrm) as Bullet;
    }

    
    public void OnPop()
    {
        
    }

    public void OnPush()
    {
    }
}