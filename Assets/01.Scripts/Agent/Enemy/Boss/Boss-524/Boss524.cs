using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss524StateEnum
{
    Chase,
    Attack,
    UseSkill,
}

public enum Boss524SkillEnum
{
    Dash,
    //Blast,
    //Laser,
}

public class Boss524 : Boss<Boss524>
{
    [HideInInspector]
    public Transform cannonTrm;
    [field: SerializeField]
    public LinePatternVisual LinePatternVisual { get; private set; }

    public EnemySkillManager<Boss524> SkillManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        cannonTrm = transform.Find("CannonVisual");
        StateMachine.Initialize(Boss524StateEnum.Chase);
        SkillManager = new EnemySkillManager<Boss524>(this);
    }
}
