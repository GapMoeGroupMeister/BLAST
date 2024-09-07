using Crogen.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour, IPoolingObject
{
	[SerializeField] private float _duration = 10f;

	[SerializeField]
	protected List<ParticleSystem> _particles;

	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public void OnParticleSystemStopped()
	{
		this.Push();
	}

	public void OnPop()
	{
		StartCoroutine(CoroutineOnDie());
		if (_particles != null)
			_particles.ForEach(p => p.Play());
	}

	public void OnPush()
	{
		StopAllCoroutines();

		if (_particles != null)
			_particles.ForEach(p => p.Simulate(0));
	}

	private IEnumerator CoroutineOnDie()
	{
		yield return new WaitForSeconds(_duration);
		this.Push();
	}
}
