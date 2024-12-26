using Crogen.CrogenPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss524BlastSkill : EnemySkill<Boss524>
{
    private Coroutine _skillCoroutine;

    private Collider[] _colliders;
    private int _maxDetactCount = 1;

    public Boss524BlastSkill(Boss524 owner, EnemySkillManager<Boss524> skillManager) : base(owner, skillManager)
    {
        _cooltime = 10f;
        _beforeDelay = 3f;
        _afterDelay = 2f;
        _lastUseTime = -_cooltime;

        _colliders = new Collider[_maxDetactCount];
    }

    public override bool IsUseable()
    {
        if (Time.time >= _lastUseTime + _cooltime)
        {
            if (Vector3.Distance(_owner.transform.position, _owner.targetTrm.position) <= 24)
            {
                return true;
            }
        }
        return false;
    }

    public override void StopSkill()
    {
        if(_skillCoroutine !=null)
        {
            _owner.StopCoroutine(_skillCoroutine);
        }
        _lastUseTime = Time.time;
        _skillManager.SetUsingSkill(false);
    }

    public override void UseSkill()
    {
        base.UseSkill();
        _skillCoroutine = _owner.StartCoroutine(BlastSkillCoroutine());
    }

    private IEnumerator BlastSkillCoroutine()
    {
        _owner.CirclePatternVisual.StartCirclePattern(_owner.transform.position, 5, 2.5f, 0.5f);
        yield return new WaitForSeconds(_beforeDelay);
        _owner.gameObject.Pop(EffectPoolType.GroundBurstEffect, _owner.transform.position, Quaternion.identity);
        int count = Physics.OverlapSphereNonAlloc(_owner.transform.position, 40, _colliders, _owner.whatIsPlayer);
        if (count > 0)
        {
            if(_colliders[0].TryGetComponent(out IDamageable health))
            {
                health.TakeDamage(10);
            }
        }
        _lastUseTime = Time.time;
        _skillManager.SetUsingSkill(false);
    }
}