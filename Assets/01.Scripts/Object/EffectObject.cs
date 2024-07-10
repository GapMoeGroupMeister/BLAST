using Crogen.ObjectPooling;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class EffectObject : MonoBehaviour, IPoolingObject
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private VisualEffect _visualEffects;

    [SerializeField] private float _lifeTime;
    private bool _isStarted;

    public PoolType OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }


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
        this.Push();
        
    }

    public void OnPop()
    {
    }

    public void OnPush()
    {
        _particles.Stop();
        _visualEffects.Stop();
        _isStarted = false;
    }
}