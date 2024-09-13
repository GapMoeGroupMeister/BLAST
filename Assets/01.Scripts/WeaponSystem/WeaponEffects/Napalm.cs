using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Napalm : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _lifeTime;
    public bool canUse;
    private DamageCaster _damageCaster;
    private ParticleSystem _explodeVFX;

    private void Awake(){
        _damageCaster = GetComponent<DamageCaster>();
        _explodeVFX = GetComponentInChildren<ParticleSystem>();
    }



    public void Explode(Vector3 pos)
    {
        canUse = true;
        transform.position = pos;
        _damageCaster.CastDamage(_damage); 
        _explodeVFX.Play();
        StartCoroutine(ExplodeCoroutine());

    }

    private IEnumerator ExplodeCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        canUse = false;
    }



}
