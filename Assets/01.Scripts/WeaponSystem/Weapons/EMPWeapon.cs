using Crogen.ObjectPooling;
using EffectSystem;
using UnityEngine;

public class EMPWeapon : Weapon
{
    [SerializeField] private SphereDamageCaster _damageCaster;
    [SerializeField] private ParticleSystem _empEffect;
    [SerializeField] private int _defaultDamage = 1;


    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            _damageCaster.CastDamage(_defaultDamage);
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
