using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Boss: Enemy
{
    [SerializeField]
    protected StatDataSO _secondStat;
    protected override void Awake()
    {
        base.Awake();
        HealthCompo.OnHealthChangedEvent.AddListener(HandleOnHealthChangedEvent);
    }

    private void HandleOnHealthChangedEvent(int prev, int cur)
    {
        if(cur <= Mathf.CeilToInt(Stat.GetValue(StatEnum.MaxHP)) / 30)
        {
            Stat = _secondStat;
        }
    }
}
