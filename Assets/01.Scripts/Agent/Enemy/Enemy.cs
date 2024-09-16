using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Enemy : Agent, IPoolingObject
{
    public EnemyMovement EnemyMovementCompo { get; private set; }

    [Header("Common Setting")]
    public LayerMask whatIsPlayer;

    [Header("Attack Setting")]
    public float attackDistance;
    public Transform targetTrm;
    [HideInInspector]
    public CapsuleCollider capsuleCollider;

    public float StunTime { get; protected set; }

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

    public abstract void Stun(float duration);

    public virtual void OnPop()
    {
        CanStateChangeable = true;
    }

    public virtual void OnPush()
    {
    }
}
