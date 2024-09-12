using EffectSystem;
using UnityEngine;

public class EMPWeapon : Weapon
{
    [SerializeField] private SphereDamageCaster _damageCaster;
    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            _damageCaster.CastDamage((int)(level * 10));
            // Stun effect
        }	

        return true;
    }
    
    public void ChangeCooldown(float value)
    {
        _cooldown = value;
    }
}
