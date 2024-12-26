using UnityEngine;
using Crogen.CrogenPooling;

public class DragoonEnemyAttackState : EnemyState<DragoonEnemy>
{
    public DragoonEnemyAttackState(DragoonEnemy enemyBase, EnemyStateMachine<DragoonEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    private float _lastAttackTime = 0;
    private Vector3 _targetPos;
    private ParticlePlayer _effect;

    public override void Enter()
    {
        base.Enter();
        _enemyBase.transform.rotation = Quaternion.LookRotation((_enemyBase.targetTrm.position - _enemyBase.transform.position).normalized);
        // EffectPlayer effect = _enemyBase.gameObject.Pop(PoolType.VFX_Charge, _enemyBase.firePosTrm, Vector3.zero, Quaternion.identity) as EffectPlayer;
        // effect.StartPlay(4f);
        _targetPos = _enemyBase.targetTrm.position;
        _targetPos *= 5;
        _targetPos.y = _enemyBase.firePosTrm.position.y;
        _enemyBase.MovementCompo.StopImmediately();
    }

    public override void Exit()
    {
        _effect = null;
        _lastAttackTime = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(IsTriggerCalled(AnimationTriggerEnum.EffectTrigger))
        {
            // _effect = _enemyBase.gameObject.Pop
            //     (PoolType.DragoonLaser, 
            //     _enemyBase.firePosTrm.position, 
            //     _enemyBase.transform.rotation) as ParticlePlayer;
            RemoveTrigger(AnimationTriggerEnum.EffectTrigger);
        }
        if(IsTriggerCalled(AnimationTriggerEnum.AttackTrigger))
		{
            Vector3 startPos = _enemyBase.transform.position;
            startPos.y = 0;
            Vector3 direction = _targetPos - startPos;
            _enemyBase.CastDamage();
            RemoveTrigger(AnimationTriggerEnum.AttackTrigger);
        }
        if (IsTriggerCalled(AnimationTriggerEnum.EndTrigger))
        {
            _stateMachine.ChangeState(DragoonEnemyStateEnum.Battle);
        }
    }
}
