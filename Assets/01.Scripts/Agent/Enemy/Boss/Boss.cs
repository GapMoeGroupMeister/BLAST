using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss<T> : Enemy where T : Boss<T>
{
    public EnemyStateMachine<T> StateMachine { get; protected set; }

    public StatDataSO firstStat;
    public StatDataSO secondStat;
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<T>(this);
        HealthCompo.OnHealthChangedEvent.AddListener(HandleOnHealthChangedEvent);
        Stat = firstStat;
    }

    private void HandleOnHealthChangedEvent(int prev, int cur)
    {
        if(cur <= Mathf.CeilToInt(Stat.GetValue(StatEnum.MaxHP)) / 30)
        {
            Stat = secondStat;
        }
    }

    public override void AnimationEndTrigger(AnimationTriggerEnum triggerBit)
    {
        StateMachine.CurrentState.AnimationTrigger(triggerBit);
    }
}
