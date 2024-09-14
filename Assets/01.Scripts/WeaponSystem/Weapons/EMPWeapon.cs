using Crogen.ObjectPooling;
using EffectSystem;
using UnityEngine;

public class EMPWeapon : Weapon
{
    [SerializeField] private SphereDamageCaster _damageCaster;
    [SerializeField] private PoolType _explosionEffectPoolType;
    [SerializeField] private int _defaultDamage = 1;

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
            _damageCaster.CastDamage(_defaultDamage);
            if (player == null) Debug.LogError("Player is null");
            gameObject.Pop(_explosionEffectPoolType, player.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            // Stun effect
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
