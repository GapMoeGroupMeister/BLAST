using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss524StateEnum
{
    Chase,
    Attack,
    UseSkill,
    Stun
}

public enum Boss524SkillEnum
{
    Dash,
    Blast,
    Laser,
}

public class Boss524 : Boss<Boss524>
{
    [HideInInspector]
    public Transform cannonTrm;
    public List<EnemyLaser> laserVisualList;
    [field: SerializeField]
    public LinePatternVisual LinePatternVisual { get; private set; }
    [field: SerializeField]
    public CirclePatternVisual CirclePatternVisual { get; private set; }

    public EnemySkillManager<Boss524> SkillManager { get; private set; }

    public override void OnDie()
    {
        CanStateChangeable = false;
        //DeadState�� �ΰ�
    }

    public override void Stun(float duration)
    {
        StunTime = duration;
        StateMachine.ChangeState(Boss524StateEnum.Stun);
    }

    protected override void Awake()
    {
        base.Awake();
        cannonTrm = transform.Find("CannonVisual");
        StateMachine.Initialize(Boss524StateEnum.Chase);
        SkillManager = new EnemySkillManager<Boss524>(this);
    }
}
