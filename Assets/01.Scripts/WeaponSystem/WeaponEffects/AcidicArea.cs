using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AcidicArea : WeaponEffect
{
    [SerializeField] private ParticleSystem _acidicAreaEffect;
    [SerializeField] private SphereDamageCaster _damageCaster;
    [SerializeField] private float _radius = 5;
    [SerializeField] private float _acidicAreaDuration = 5;
    [SerializeField] private int _acidicAreaDamage = 1;
    [SerializeField] private float _tickCool = 0.5f;
    
    public float AcidicAreaDuration => _acidicAreaDuration;
    
    
    public void SpawnAcidicArea()
    {
        _acidicAreaEffect.Play();
        StartCoroutine(GetAcidicArea());
    }
    
    private IEnumerator GetAcidicArea()
    {
        DOTween.To(() => _damageCaster.radius, x => _damageCaster.radius = x, _radius, 0.5f);
        float timer = 0;
        float coolTimer = 0;
        while(timer < _acidicAreaDuration)
        {
            if (coolTimer >= _tickCool)
            {
                _damageCaster.CastDamage(_acidicAreaDamage);
                Debug.Log("Acidic Area Damage");
                coolTimer = 0;
            }
            timer += Time.deltaTime;
            coolTimer += Time.deltaTime;
            yield return null;
        }
        DOTween.To(() => _damageCaster.radius, x => _damageCaster.radius = x, 0, 0.5f);
    }
}