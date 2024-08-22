using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Agent, IPoolingObject
{
    public EnemyMovement EnemyMovementCompo { get; private set; }

    [Header("Common Setting")]
    public float moveSpeed;

    protected float _defaultMoveSpeed;

    [SerializeField]
    protected LayerMask _whatIsPlayer;
    [SerializeField]
    protected LayerMask _whatIsObstacle;

    [Header("Attack Setting")]
    public float runAwayDistance;
    public float attackDistance;
    public float attackCooldown;
    [SerializeField]
    protected int _maxCheckEnemy = 1;
    [HideInInspector]
    public float lastAttackTime;

    [HideInInspector]
    public Transform targetTrm;
    [HideInInspector]
    public CapsuleCollider capsuleCollider;

    protected Collider[] _enemyCheckColliders;

    public PoolType OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }

    protected override void Awake()
    {
        base.Awake();

        _defaultMoveSpeed = moveSpeed;

        _enemyCheckColliders = new Collider[_maxCheckEnemy];
        capsuleCollider = GetComponent<CapsuleCollider>();
        EnemyMovementCompo = MovementCompo as EnemyMovement;
        EnemyMovementCompo.Initialize(this);
    }

    public virtual Collider IsPlayerDetected()
    {
        int cnt = Physics.OverlapSphereNonAlloc(transform.position, runAwayDistance, _enemyCheckColliders, _whatIsPlayer);

        return cnt >= 1 ? _enemyCheckColliders[0] : null;
    }

    public virtual bool IsObstacleDetected(float distance, Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, distance, _whatIsObstacle);
    }

    public abstract void AnimationEndTrigger(AnimationTriggerEnum triggerBit);

    public virtual void OnPop()
    {
    }

    public virtual void OnPush()
    {
    }
}
