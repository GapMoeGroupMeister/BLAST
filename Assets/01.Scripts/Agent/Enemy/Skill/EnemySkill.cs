using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class EnemySkill<T> where T : Enemy
{
    protected T _owner;
    protected EnemySkillManager<T> _skillManager;

    protected float _lastUseTime = 0;
    protected float _cooltime;
    protected float _speed;
    protected float _beforeDelay;
    protected float _afterDelay;


    public EnemySkill(T owner, EnemySkillManager<T> skillManager)
    {
        _owner = owner;
        _skillManager = skillManager;
    }

    public EnemySkill(T owner, float cooltime, float speed, float beforeDelay = 0, float afterDelay = 0)
    {
        _owner = owner;
        _cooltime = cooltime;
        _speed = speed;
        _beforeDelay = beforeDelay;
        _afterDelay = afterDelay;
    }


    public abstract bool IsUseable();

    public virtual void UseSkill()
    {
        _skillManager.SetUsingSKill(true);
    }

}