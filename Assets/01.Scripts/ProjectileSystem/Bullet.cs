using UnityEngine;
using Crogen.ObjectPooling;
using System.Collections;

public class Bullet : MonoBehaviour, IPoolingObject
{
	public float speed = 100f;
	public float duration = 5f;
	public bool isPenetration = false; 

	[SerializeField] protected DamageCaster _damageCaster;
	[SerializeField] protected int _damage = 1;
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	protected virtual void Awake()
	{
		_damageCaster.OnDamageCastSuccessEvent += OnDie;
	}

	protected virtual void FixedUpdate()
	{
		_damageCaster.CastDamage(_damage);
	}

	public virtual void OnPop()
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
		Vector3 endPos = transform.position + transform.forward * speed * duration;
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
		if (isPenetration) return;
		this.Push();
	}
}
