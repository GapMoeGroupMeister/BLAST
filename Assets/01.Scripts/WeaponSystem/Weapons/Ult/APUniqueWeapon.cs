using System;
using System.Collections;
using System.Collections.Generic;
using Crogen.CrogenPooling;
using UnityEngine;

public class APUniqueWeapon : UniqueWeapon
{
    [SerializeField] private EffectPoolType _chargeEffectPoolType;
    [SerializeField] private EffectPoolType _empExplodeEffectPoolType;
    private Player _player;

    private void Awake()
    {
        _player = GameManager.Instance.Player;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void WeaponInit()
    {
        base.WeaponInit();
    }

    protected override void UseUltWeapon()
    {
        if (UseWeapon() == false) return;
        StartCoroutine(CoroutineExplode());
    }

    private IEnumerator CoroutineExplode()
    {
        gameObject.Pop(_chargeEffectPoolType, _player.transform);
        CameraShakeController.Instance.ShakeCam(3f, 3f);
        yield return new WaitForSeconds(3f);
        gameObject.Pop(_empExplodeEffectPoolType, _player.transform.position, Quaternion.identity);
        CameraShakeController.Instance.ShakeCam(21, 1.5f);
    }
}
