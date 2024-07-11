using Crogen.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectPlayer : MonoBehaviour, IPoolingObject
{
    [SerializeField]
    protected List<ParticleSystem> _particles;
    [SerializeField]
    protected List<VisualEffect> _effects;

    public PoolType OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }

    public void StartPlay(float endTime)
    {
        if (_particles != null)
            _particles.ForEach(p => p.Play());
        if (_effects != null)
            _effects.ForEach(e => e.Play());

        StartCoroutine(TimerCoroutine(endTime));
    }

    private IEnumerator TimerCoroutine(float endTime)
    {
        yield return new WaitForSeconds(endTime);
        this.Push();
    }

    public void OnPop()
    {
    }

    public void OnPush()
    {
        if (_particles != null)
            _particles.ForEach(p => p.Simulate(0));
        if (_effects != null)
            _effects.ForEach(e => e.Stop());
    }
}
