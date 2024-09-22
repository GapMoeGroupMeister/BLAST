using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss524LaserSkill : EnemySkill<Boss524>
{
    private Coroutine _skillCoroutine;

    public Boss524LaserSkill(Boss524 owner, EnemySkillManager<Boss524> skillManager) : base(owner, skillManager)
    {
        _cooltime = 15f;
        _beforeDelay = 1f;
        _afterDelay = 2f;
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
        if(_skillCoroutine != null)
        {
            _owner.StopCoroutine(_skillCoroutine);
        }
        _lastUseTime = Time.time;
        _skillManager.SetUsingSkill(false);
        _owner.laserVisualList.ForEach(x => x.gameObject.SetActive(false));
    }

    public override void UseSkill()
    {
        base.UseSkill();
        _skillCoroutine = _owner.StartCoroutine(LaserSkillCoroutine());
    }

    private IEnumerator LaserSkillCoroutine()
    {
        _owner.laserVisualList.ForEach(x => x.gameObject.SetActive(true));
        _owner.laserVisualList.ForEach(x => x.EnableLaser(400f));
        float percent = 0;
        Vector2 dir = new Vector2(_owner.targetTrm.position.x, _owner.targetTrm.position.z) - new Vector2(_owner.transform.position.x, _owner.transform.position.z);
        dir.Normalize();
        _owner.cannonTrm.rotation = Quaternion.AngleAxis(180, Vector3.up);
        yield return new WaitForSeconds(_beforeDelay);
        while (percent < 1)
        {
            float y = Mathf.Lerp(0, 360, percent);
            _owner.cannonTrm.rotation = Quaternion.AngleAxis(180 + y * EaseFunc.InCubic(percent), Vector3.up);
            percent += Time.deltaTime * (1 / 1.5f);
            yield return null;
        }
        _owner.laserVisualList.ForEach(x => x.gameObject.SetActive(false));
        yield return new WaitForSeconds(_afterDelay);
        _owner.cannonTrm.localRotation = Quaternion.identity;
        _lastUseTime = Time.time;
        _skillManager.SetUsingSkill(false);
    }
}
