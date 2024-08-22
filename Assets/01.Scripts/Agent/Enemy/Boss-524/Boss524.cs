using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss524EnemyState
{
    Chase,
    Dash,
    Scatter,
}
public class Boss524 : Enemy
{
    public EnemyStateMachine<Boss524> StateMachine { get; private set; }

    [HideInInspector]
    public Transform cannonTrm;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<Boss524>(this);

        cannonTrm = transform.Find("CannonVisual");
    }

    public override void AnimationEndTrigger(AnimationTriggerEnum triggerBit)
    {
        StateMachine.CurrentState.AnimationTrigger(triggerBit);
    }
}
