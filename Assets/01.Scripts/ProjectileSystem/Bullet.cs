using UnityEngine;
using Crogen.ObjectPooling;
using System.Collections;
using UnityEngine.Events;

public class Bullet : MonoBehaviour, IPoolingObject
{
	public float speed = 100f;
	public float duration = 5f;
	public bool isPenetration = false;
	public float penetrationPercent = 0f;
	[SerializeField] protected DamageCaster _damageCaster;
	[SerializeField] protected int _damage = 1;
	public UnityEvent OnFireEvent;
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	protected virtual void Awake()
	{
		_damageCaster.OnCasterSuccessEvent += OnCollision;
	}

	protected virtual void OnDestroy()
	{
		_damageCaster.OnCasterSuccessEvent -= OnCollision;
	}

	protected virtual void FixedUpdate()
	{
		_damageCaster.CastDamage(_damage);
	}

	public virtual void OnPop()
	{
		OnFireEvent?.Invoke();
		StartCoroutine(CoroutineMove());
	}

	public void OnPush()
	{
		StopAllCoroutines();
	}

	private void OnCollision()
	{
		if (isPenetration)
		{
			float random = Random.Range(0f, 1f);
			if(penetrationPercent < random)
			{
				return;
			}
		}
		OnDie();
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
		this.Push();
	}
}
