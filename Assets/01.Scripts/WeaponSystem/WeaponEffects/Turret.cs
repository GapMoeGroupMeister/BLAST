using System;
using System.Collections;
using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class Turret : WeaponEffect, IPoolingObject
{
    [Header("Turret Trm Setting")]
    [SerializeField] private Transform _turretHeadTrm;
    [SerializeField] private Transform _turretFireTrm;
    
    [Header("Turret Setting")]
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private PoolType _bulletPoolType;
    [SerializeField] private float _enemyFindRadius = 20f;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _dieTime;
    
    [Space(10)]
    [SerializeField] private ParticleSystem _bombEffect;
    
    public PoolType OriginPoolType { get; set; }
    public GameObject gameObject { get; set; }
    public bool IsPushed { get; private set; }
    
    private float _fireRateTimer;
    private Collider[] _colliders = new Collider[2];
    private Transform _target;

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
        else
        {
            SetTarget(GetNearestEnemy(transform.position));
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
        gameObject.Pop(_bulletPoolType, _turretFireTrm.position, _turretHeadTrm.rotation);
    }

    
    public void OnPop()
    {
        IsPushed = false;
        SetTarget(GetNearestEnemy(transform.position));
        SpawnAnimation(new Vector3(5f, 5f, 5f), Ease.OutBounce);
        StartCoroutine(AutoDie());
    }

    private IEnumerator AutoDie()
    {
        yield return new WaitForSeconds(_dieTime);
        SpawnAnimation(Vector3.zero, Ease.InSine);
        Instantiate(_bombEffect, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        this.Push();
    }

    public void OnPush()
    {
        IsPushed = true;
    }
    
    private Transform GetNearestEnemy(Vector3 pos)
    {
        int count = Physics.OverlapSphereNonAlloc(pos, _enemyFindRadius, _colliders, whatIsEnemy);
		
        if (count == 0)
            return null;
        return _colliders[0].transform;

    }

    private void SpawnAnimation(Vector3 endScale, Ease ease)
    {
        transform.DOScale(endScale, 0.5f).SetEase(ease);
    }
}