using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Boss524DashSkill : EnemySkill<Boss524>
{
    private Coroutine _skillCoroutine;
    private Tween _moveTween;

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
            if (Vector3.Distance(_owner.transform.position, _owner.targetTrm.position) > 50)
            {
                return true;
            }
        }
        return false;
    }

    public override void StopSkill()
    {
        if (_skillCoroutine != null)
            _owner.StopCoroutine(_skillCoroutine);
        if (_moveTween != null && _moveTween.IsActive())
            _moveTween.Kill();
        _owner.LinePatternVisual.gameObject.SetActive(false);
        _lastUseTime = Time.time;
        _skillManager.SetUsingSkill(false);
    }

    public override void UseSkill()
    {
        base.UseSkill();
        _skillCoroutine = _owner.StartCoroutine(DashSkillCoroutine());
    }

    private IEnumerator DashSkillCoroutine()
    {
        Vector3 startPos = _owner.transform.position;
        Vector3 endPos = _owner.targetTrm.position;
        Vector3 dir = endPos - startPos;
        Vector3 destination = startPos + dir.normalized * 60;
        _owner.transform.rotation = Quaternion.LookRotation(dir.normalized);
        _owner.LinePatternVisual.gameObject.SetActive(true);
        _owner.LinePatternVisual.StartLinePattern(startPos, destination, 1.5f, 0.5f, 1.5f);
        yield return new WaitForSeconds(_beforeDelay);
        _owner.ContactHitCompo.SetActive(true);
        _moveTween = _owner.transform.DOMove(destination, 1.3f);
        _moveTween.OnUpdate(() =>
        {
            _owner.dashEffectCaster.CreateDashEffect();
        });
        yield return new WaitForSeconds(1.3f + _afterDelay);
        _owner.ContactHitCompo.SetActive(false);
        _skillManager.SetUsingSkill(false);
        _lastUseTime = Time.time;
    }
}
