using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Boss524DashSkill : EnemySkill<Boss524>
{
    public Boss524DashSkill(Boss524 owner, EnemySkillManager<Boss524> skillManager) : base(owner, skillManager)
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
            if (Vector3.Distance(_owner.transform.position, _owner.targetTrm.position) <= 200)
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
        Vector3 startPos = _owner.transform.position;
        Vector3 endPos = _owner.targetTrm.position;
        Vector3 dir = endPos - startPos;
        Vector3 destination = startPos + dir.normalized * 60;
        _owner.transform.rotation = Quaternion.LookRotation(dir.normalized);
        _owner.LinePatternVisual.StartLinePattern(startPos, destination, 1.5f, 0.5f, 1.5f);
        yield return new WaitForSeconds(_beforeDelay);
        _owner.transform.DOMove(destination, 0.6f);
        yield return new WaitForSeconds(0.6f + _afterDelay);
        _skillManager.SetUsingSKill(false);
        _lastUseTime = Time.time;
    }
}
