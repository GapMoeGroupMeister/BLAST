using Crogen.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss524DeadState : EnemyState<Boss524>
{
    private Material _dissolveMat;
    private readonly int _burnedHash = Shader.PropertyToID("_Burned");

    public Boss524DeadState(Boss524 enemyBase, EnemyStateMachine<Boss524> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _dissolveMat = enemyBase.RendererCompo.material;
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
        XPManager.Instance.CreateXP(_enemyBase.transform.position, (XPType)(int)(_enemyBase.Level * 3));
        int rand = Random.Range(0, 100);
        if (rand < 30)
        {
            ResourceManager.CreateCoin(_enemyBase.transform.position);
        }
        GameObject.Destroy(_enemyBase.gameObject);
    }
}
