using System;
using System.Collections;
using UnityEngine;

public class DestructionBeam : WeaponEffect
{
    [SerializeField] private ParticleSystem _chargeEffect;
    [SerializeField] private ParticleSystem _beamEffect;
    [SerializeField] private BoxDamageCaster _damageCaster;
    
    [ContextMenu("StartBeam")]
    public void TestB()
    {
        StartBeam(1, 1, transform);
    }
    
    public void StartBeam(float chargeTime, float beamDuration, Transform t)
    {
        transform.SetParent(t);
        StartCoroutine(ChargeBeam(chargeTime, beamDuration));
    }
    
    private IEnumerator ChargeBeam(float chargeTime, float beamDuration)
    {
        _chargeEffect.Play();
        yield return new WaitForSeconds(chargeTime);
        _beamEffect.Play();
        yield return new WaitForSeconds(beamDuration);
        _beamEffect.Stop();
    }

    private void FixedUpdate()
    {
        if (_beamEffect.isPlaying)
        {
            _damageCaster.CastDamage(_damage);
        }
    }
}