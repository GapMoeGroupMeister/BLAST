using System.Collections;
using ItemManage;
using UnityEngine;

public class BarrierItem : Item
{
    private void Start()
    {
        OnInteractEvent += HandleBarrierAdd;
    }

    private void HandleBarrierAdd()
    {
        // Add Barrier
        StartCoroutine(BarrierOnCoroutine());
    }

    private IEnumerator BarrierOnCoroutine()
    {
        _player.HealthCompo.IsInvincibility = true;
        yield return new WaitForSeconds(_itemEffectDuration);
        _player.HealthCompo.IsInvincibility = false;
    }
}