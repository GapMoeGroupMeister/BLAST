using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill<T> where T : Enemy
{
    public EnemySkill(T owner)
    {
        _owner = owner;
    }

    public EnemySkill(T owner, float cooltime, float speed, float beforeDelay = 0, float afterDelay = 0)
    {
        _owner = owner;
        _cooltime = cooltime;
        _speed = speed;
        _beforeDelay = beforeDelay;
        _afterDelay = afterDelay;
    }

    private T _owner;

    private float _cooltime;
    private float _cooldownTimer = 0;
    private float _speed;
    private float _beforeDelay;
    private float _afterDelay;


    public bool IsUseable { get; protected set; }

    public void UpdateCooltime()
    {
        if (IsUseable) return;
        _cooldownTimer += Time.deltaTime;
        if (_cooldownTimer > _cooltime)
        {
            IsUseable = true;
        }
    }

    public virtual void UseSkill()
    {
        _cooldownTimer = 0;
    }
}