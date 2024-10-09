using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss524LaserSkill : EnemySkill<Boss524>
{
    private Coroutine _skillCoroutine;

    public Boss524LaserSkill(Boss524 owner, EnemySkillManager<Boss524> skillManager) : base(owner, skillManager)
    {
        _cooltime = 20f;
        _lastUseTime = -_cooltime;
    }

    public override bool IsUseable()
    {
        if (Time.time >= _lastUseTime + _cooltime)
        {
            if (Vector3.Distance(_owner.transform.position, _owner.targetTrm.position) <= 50)
            {
                return true;
            }
        }
        return false;
    }

    public override void StopSkill()
    {
    }

    public override void UseSkill()
    {
        base.UseSkill();
        _owner.StartCoroutine(SkillCoroutine());
    }

    private IEnumerator SkillCoroutine()
    {
        _owner.LaserAlignerCompo.SetActiveLaser(true, Mathf.CeilToInt(_owner.Level * 3) + 1);
        _owner.LaserAlignerCompo.transform.DOMoveY(15, 1f);
        yield return new WaitForSeconds(0.8f);
        _skillManager.SetUsingSkill(false);
        _lastUseTime = Time.time;
    }
}
