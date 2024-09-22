using Crogen.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemyDeadState : EnemyState<WizardEnemy>
{
    private Material _dissolveMat;
    private readonly int _burnedHash = Shader.PropertyToID("_Burned");

    public WizardEnemyDeadState(WizardEnemy enemyBase, EnemyStateMachine<WizardEnemy> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _dissolveMat = enemyBase.MeshRendererCompo.material;
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.EnemyMovementCompo.DisableNavAgent();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (IsTriggerCalled(AnimationTriggerEnum.EndTrigger))
        {
            _enemyBase.StartCoroutine(BurnedCoroutine());
            RemoveTrigger(AnimationTriggerEnum.EndTrigger);
        }
    }

    private IEnumerator BurnedCoroutine()
    {
        float percent = 0;
        while (percent < 1)
        {
            _dissolveMat.SetFloat(_burnedHash, percent);
            percent += Time.deltaTime * 2;
            yield return null;
        }
        _enemyBase.Push();
    }
}
