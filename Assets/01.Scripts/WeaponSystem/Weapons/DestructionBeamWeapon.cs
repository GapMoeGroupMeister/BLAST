using System.Collections;
using UnityEngine;

public class DestructionBeamWeapon : Weapon
{
    [SerializeField] private BoxDamageCaster _leftBoxDamageCaster;
    [SerializeField] private BoxDamageCaster _rightBoxDamageCaster;
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
        yield return new WaitForSeconds(_chargeTime);
        yield return StartCoroutine(FireBeam());
    }

    private IEnumerator FireBeam()
    {
        float timer = 0;
        while (timer < _beamDuration)
        {
            _leftBoxDamageCaster.CastDamage(_damage);
            _rightBoxDamageCaster.CastDamage(_damage);
            Debug.Log("Destruction Beam Damage");
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
