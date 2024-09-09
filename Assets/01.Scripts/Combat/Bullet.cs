using UnityEngine;
using Crogen.ObjectPooling;
using System.Collections;

public class Bullet : MonoBehaviour, IPoolingObject, ISizeupable
{
	public float speed = 100f;
	public float duration = 5f;

	[SerializeField] protected DamageCaster _damageCaster;
	[SerializeField] protected int _damage = 1;
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public float MultipliedCount { get; set; }

	protected virtual void Awake()
	{
		_damageCaster.OnDamageCastSuccessEvent += OnDie;
	}

	protected virtual void Start()
	{
		(WeaponManager.Instance.GetWeapon(WeaponType.BulletSizeUp) as BulletSizeUpWeapon).ResizeEvent += OnResize;
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
		this.Push();
	}

	public void OnResize(float multipliedCount)
	{
		MultipliedCount = multipliedCount;
		transform.localScale = Vector3.one * MultipliedCount;
	}
}
