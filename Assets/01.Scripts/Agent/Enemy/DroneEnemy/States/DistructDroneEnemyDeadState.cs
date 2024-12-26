using Crogen.CrogenPooling;
using UnityEngine;

public class DistructDroneEnemyDeadState : EnemyState<DistructDroneEnemy>
{
    public DistructDroneEnemyDeadState(DistructDroneEnemy enemyBase, EnemyStateMachine<DistructDroneEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.CastDamage();
        XPManager.Instance.CreateXP(_enemyBase.transform.position, (XPType)(int)(_enemyBase.Level * 4));
        int rand = Random.Range(0, 100);
        if (rand < 30)
        {
            ResourceManager.CreateCoin(_enemyBase.transform.position);
        }
        _enemyBase.Push();
    }
}
