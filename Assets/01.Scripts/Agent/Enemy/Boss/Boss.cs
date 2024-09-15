using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Boss<T> : Enemy where T : Boss<T>
{
    public EnemyStateMachine<T> StateMachine { get; protected set; }

    [SerializeField]
    protected StatDataSO _secondStat;
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<T>(this);
        HealthCompo.OnHealthChangedEvent.AddListener(HandleOnHealthChangedEvent);
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    private void HandleOnHealthChangedEvent(int prev, int cur)
    {
        if(cur <= Mathf.CeilToInt(Stat.GetValue(StatEnum.MaxHP)) / 30)
        {
            Stat = _secondStat;
        }
    }

    public override void AnimationEndTrigger(AnimationTriggerEnum triggerBit)
    {
        StateMachine.CurrentState.AnimationTrigger(triggerBit);
    }
}
