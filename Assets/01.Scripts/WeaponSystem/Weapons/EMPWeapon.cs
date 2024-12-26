using System;
using Crogen.CrogenPooling;
using EffectSystem;
using UnityEngine;

public class EMPWeapon : Weapon
{
    [SerializeField] private SphereDamageCaster _damageCaster;
    [SerializeField] private ParticleSystem _empEffect;
    [SerializeField] private int _defaultDamage = 1;

    private void Awake()
    {
        WeaponInit();
    }

    public override void WeaponInit()
    {
        base.WeaponInit();
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            SphereDamageCaster sphereDamageCaster = Instantiate(_damageCaster, player.transform.position, Quaternion.identity);
            sphereDamageCaster.CastDamage(_defaultDamage);
            _empEffect.Play();
        }	

        return true;
    }
    
    public void ChangeCooldown(float value)
    {
        _cooldown = value;
    }
    
    public void ChangeDamage(int value)
    {
        _defaultDamage = value;
    }
}
