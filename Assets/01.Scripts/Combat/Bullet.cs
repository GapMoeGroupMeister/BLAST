using UnityEngine;
using Crogen.ObjectPooling;
using System.Collections;

public class Bullet : MonoBehaviour, IPoolingObject
{
	public float speed = 100f;
	public float duration = 5f;

	[SerializeField] private DamageCaster _damageCaster;
	[SerializeField] private int _damage = 1;
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	private void Awake()
	{
		_damageCaster.OnDamageCastSuccessEvent += OnDie;
	}

	private void FixedUpdate()
	{
		_damageCaster.CastDamage(_damage);
	}

	public void OnPop()
	{
		StartCoroutine(CoroutineMove());
	}

	public void OnPush()
	{
		StopAllCoroutines();
	}

	private IEnumerator CoroutineMove()
	{
		float currentTime = 0;
		float percent = 0;
		Vector3 startPos = transform.position;
		Vector3 endPos = transform.forward * speed * duration;
		while (percent < 1)
		{
			yield return null;
			currentTime += Time.deltaTime;
			percent = currentTime / duration;
			transform.position = Vector3.Lerp(startPos, endPos, percent);
		}
		yield return new WaitForSeconds(duration);
		OnDie();
	}

	private void OnDie()
	{
		this.Push();
	}
}
