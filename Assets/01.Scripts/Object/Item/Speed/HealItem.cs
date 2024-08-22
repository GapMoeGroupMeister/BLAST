using System;
using ItemManage;
using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private float _healValue;
    private void Start()
    {
        OnInteractEvent += HandleHeal;
    }

    private void HandleHeal()
    {
        Debug.Log($"Heal {_healValue}");
    }
}