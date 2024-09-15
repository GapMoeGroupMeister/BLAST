using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticBlock : MonoBehaviour
{
    
    public float pullPower = 4f;
    public float detectRange = 10f;
    public float duration = 3f;
    [SerializeField] private LayerMask _enemyLayer;
    private Collider[] _colliders;
    private ParticleSystem _generateVFX;
    private ParticleSystem _magneticVFX;
    private bool _isActive;
    

    private void Awake()
    {
        _generateVFX = transform.Find("GenerateVFX").GetComponent<ParticleSystem>();
        _magneticVFX = transform.Find("VFX").GetComponent<ParticleSystem>();
        
        _colliders = new Collider[10];
    }


    public void Active(){
        _isActive = true;
        _generateVFX.Play();
        StartCoroutine(ActiveCoroutine());
    }

    private IEnumerator ActiveCoroutine()
    {

        _magneticVFX.Play();
        yield return new WaitForSeconds(duration);
        _magneticVFX.Stop();
        _isActive = false;
    }

    private void FixedUpdate()
    {
        Pull();
    } 

    private void Pull()
    {
        int amount = Physics.OverlapSphereNonAlloc(transform.position, detectRange, _colliders, _enemyLayer);

        for (int i = 0; i < amount; i++)
        {
            Collider target = _colliders[i];
            target.transform.position += (target.transform.position - transform.position ) * Time.fixedDeltaTime * pullPower;

        }
    }
}
