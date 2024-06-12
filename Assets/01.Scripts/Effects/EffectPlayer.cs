using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using ObjectPooling;

public class EffectPlayer : PoolableMono
{
    [SerializeField]
    protected List<ParticleSystem> _particles;
    [SerializeField]
    protected List<VisualEffect> _effects;

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
        PoolingManager.Instance.Push(this);
    }

    public override void ResetItem()
    {
        if (_particles != null)
            _particles.ForEach(p => p.Simulate(0));
        if (_effects != null)
            _effects.ForEach(e => e.Stop());
    }
}
