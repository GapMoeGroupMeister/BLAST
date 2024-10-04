using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistructDroneEnemyDistructState : EnemyState<DistructDroneEnemy>
{
    public DistructDroneEnemyDistructState(DistructDroneEnemy enemyBase, EnemyStateMachine<DistructDroneEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    private float _distructTime = 1.5f;
    private float _distructTimer = 0f;

    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        _distructTimer = 0;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _distructTimer += Time.deltaTime;

        _enemyBase.redLight.intensity = _distructTimer * 70;
        
        if(_distructTimer > _distructTime)
        {
            //���� �ϴ� �ڵ�. VFX�������ʽÿ�.
            _stateMachine.ChangeState(DistructDroneEnemyStateEnum.Dead);
        }
    }
}
