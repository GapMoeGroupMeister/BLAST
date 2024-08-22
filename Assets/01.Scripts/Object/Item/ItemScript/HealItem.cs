using System;
using ItemManage;
using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private int _healValue;
    private void Start()
    {
        OnInteractEvent += HandleHeal;
    }

    private void HandleHeal()
    {
        _player.HealthCompo.RestoreHealth(_healValue);
    }
}