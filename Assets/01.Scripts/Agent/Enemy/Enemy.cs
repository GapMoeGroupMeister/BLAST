using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Agent, IPoolingObject
{
    public EnemyMovement EnemyMovementCompo { get; private set; }

    [Header("Common Setting")]
    [SerializeField]
    protected LayerMask _whatIsPlayer;

    [Header("Attack Setting")]
    public float attackDistance;
    public Transform targetTrm;
    [HideInInspector]
    public CapsuleCollider capsuleCollider;

    protected Collider[] _enemyCheckColliders;

    public PoolType OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }

    private void OnValidate()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null) targetTrm = player.transform;
    }

    protected override void Awake()
    {
        base.Awake();
        HealthCompo.OnDieEvent.AddListener(OnDie);
        capsuleCollider = GetComponent<CapsuleCollider>();
        EnemyMovementCompo = MovementCompo as EnemyMovement;
        EnemyMovementCompo.Initialize(this);
    }

    public abstract void OnDie();

    public abstract void AnimationEndTrigger(AnimationTriggerEnum triggerBit);

    public virtual void OnPop()
    {
        CanStateChangeable = true;
    }

    public virtual void OnPush()
    {
    }
}
