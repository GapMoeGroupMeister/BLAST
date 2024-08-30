using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Boss524DashSkill : EnemySkill<Boss524>
{
    public Boss524DashSkill(Boss524 owner) : base(owner)
    {
        _cooltime = 10f;
        _beforeDelay = 2f;
        _afterDelay = 2f;
        _lastUseTime = -_cooltime;
    }

    public override bool IsUseable()
    {
        if (Time.time >= _lastUseTime + _cooltime)
        {
            if(Vector3.Distance(_owner.transform.position, _owner.targetTrm.position) <= 200)
            {
                return true;
            }
        }
        return false;
    }

    public override void UseSkill()
    {
        base.UseSkill();
        _owner.StartCoroutine(DashSkillCoroutine());
    }

    private IEnumerator DashSkillCoroutine()
    {
        yield return new WaitForSeconds(_beforeDelay);
        _owner.transform.DOMove(_owner.targetTrm.position, 0.6f);
        yield return new WaitForSeconds(_afterDelay);
        IsUsing = false;
    }
}
