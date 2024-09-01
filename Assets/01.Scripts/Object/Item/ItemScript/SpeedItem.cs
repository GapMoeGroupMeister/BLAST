using System;
using System.Collections;
using ItemManage;
using UnityEngine;

public class SpeedItem : Item
{
    [SerializeField] private float _speedUpValue;

    protected override void GetEffect()
    {
        StartCoroutine(SpeedUpCoroutine());
    }

    private IEnumerator SpeedUpCoroutine()
    {
        _player.Stat.statDictionary[StatEnum.Speed] += _speedUpValue;
        yield return new WaitForSeconds(_itemEffectDuration);
        _player.Stat.statDictionary[StatEnum.Speed] -= _speedUpValue;
    }
}