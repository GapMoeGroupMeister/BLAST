using Crogen.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour, IPoolingObject
{
	[SerializeField] private float _duration = 10f;

	[SerializeField]
	protected List<ParticleSystem> _particles;

	[SerializeField]
	protected List<FeedbackPlayer> _feedbackPlayers;

	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public void OnPop()
	{
		StartCoroutine(CoroutineOnDie());
		if (_particles != null)
			_particles.ForEach(p => p.Play());

		if (_feedbackPlayers != null)
			_feedbackPlayers.ForEach(f => f.PlayFeedback()); 
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
