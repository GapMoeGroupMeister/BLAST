using System.Collections;
using UnityEngine;

public class Napalm : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    public bool canUse;
    private DamageCaster _damageCaster;
    private ParticleSystem _explodeVFX;

    private void Awake(){
        _explodeVFX = GetComponentInChildren<ParticleSystem>();
    }

    public void Explode(){
       _damageCaster.CastDamage(_damage); 
       _explodeVFX.Play();

    }

    private IEnumerator ExplodeCoroutine(){
        
    }



}
