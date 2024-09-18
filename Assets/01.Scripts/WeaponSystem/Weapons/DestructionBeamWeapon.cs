using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionBeamWeapon : Weapon
{
    [SerializeField] private ParticleSystem _chargeEffect;
    [SerializeField] private ParticleSystem _beamEffect;
    [SerializeField] private Transform[] _boxDamageCaster;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _chargeTime = 1;
    [SerializeField] private float _beamDuration = 1;

    public override bool UseWeapon()
    {
        if(base.UseWeapon())
        {
            StartCoroutine(ChargeBeam());
        }	

        return true;
    }

    private IEnumerator ChargeBeam()
    {
        foreach (var t in _boxDamageCaster)
        {
            var chargeEffect = Instantiate(_chargeEffect, t.transform);
            //chargeEffect.transform.rotation = player. 
            chargeEffect.Play();
            Destroy(chargeEffect.gameObject, _chargeTime + _beamDuration);
        }
        yield return new WaitForSeconds(_chargeTime);
        yield return StartCoroutine(FireBeam());
    }

    private IEnumerator FireBeam()
    {
        foreach (var t in _boxDamageCaster)
        {
            var beamEffect = Instantiate(_beamEffect, t.transform);
            beamEffect.Play();
            Destroy(beamEffect.gameObject, _beamDuration);
        }
        float timer = 0;
        while (timer < _beamDuration)
        {
            // foreach (var boxDamageCaster in _boxDamageCaster)
            // {
            //     boxDamageCaster.CastDamage(_damage);
            //     yield return null;
            // }
            Debug.Log("Destruction Beam Damage");
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
