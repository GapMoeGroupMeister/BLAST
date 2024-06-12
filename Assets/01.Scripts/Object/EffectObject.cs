using System.Collections;
using ObjectPooling;
using UnityEngine;
using UnityEngine.VFX;

public class EffectObject : PoolableMono
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private VisualEffect _visualEffects;

    [SerializeField] private float _lifeTime;
    private bool _isStarted;
    
    
    public override void ResetItem()
    {
        _particles.Stop();
        _visualEffects.Stop();
        _isStarted = false;
    }

    public void Play()
    {
        if (_isStarted) return;

        _isStarted = true;
        _particles.Play();
        _visualEffects.Play();
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        PoolingManager.Instance.Push(this);
        
    }
}