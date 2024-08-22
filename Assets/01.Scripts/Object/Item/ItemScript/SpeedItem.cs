using System;
using ItemManage;
using UnityEngine;

public class SpeedItem : Item
{
    [SerializeField] private float _speedUpValue;
    private void Start()
    {
        OnInteractEvent += HandleSpeedUp;
    }

    private void HandleSpeedUp()
    {
        _player.Stat.statDictionary[StatEnum.Speed] += _speedUpValue;
    }
}