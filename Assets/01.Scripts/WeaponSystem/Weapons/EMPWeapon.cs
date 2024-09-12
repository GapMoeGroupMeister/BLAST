using EffectSystem;
using UnityEngine;

public class EMPWeapon : Weapon
{
    [SerializeField] private SphereDamageCaster _damageCaster;
    [SerializeField] private ParticleSystem _explosionEffect;

    public override void WeaponInit()
    {
        if (!player)
            player = FindObjectOfType<Player>();
    }

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            WeaponInit();
            _damageCaster.CastDamage((int)(level * 10));
            if (player == null) Debug.LogError("Player is null");
            ParticleSystem explosionEffect = Instantiate(_explosionEffect, player.transform.position, Quaternion.identity);
            explosionEffect.Play();
            // Stun effect
        }	

        return true;
    }
    
    public void ChangeCooldown(float value)
    {
        _cooldown = value;
    }
}
