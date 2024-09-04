using System;
using ItemManage;
using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private int _healValue;

    protected override void GetEffect()
    {
        _player.HealthCompo.RestoreHealth(_healValue);
    }
}