using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss524State
{
    Chase,
    Attack,
}
public class Boss524 : Boss<Boss524>
{
    [HideInInspector]
    public Transform cannonTrm;

    protected override void Awake()
    {
        base.Awake();
        cannonTrm = transform.Find("CannonVisual");
        StateMachine.ChangeState(Boss524State.Chase);
    }
}
