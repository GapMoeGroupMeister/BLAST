using UnityEngine;
using Crogen.ObjectPooling;
using System.Collections;

public class Bullet : MonoBehaviour, IPoolingObject, ISizeupable
{
	public float speed = 100f;
	public float duration = 5f;

	[SerializeField] private DamageCaster _damageCaster;
	[SerializeField] private int _damage = 1;
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public BulletSizeUpWeapon BulletSizeUpWeapon { get; set; }
	public float MultipliedCount { get; set; }
	public Vector3 DefaultSize { get; set; }

	private void Awake()
	{
		_damageCaster.OnDamageCastSuccessEvent += OnDie;
	}

	private void Start()
	{
		BulletSizeUpWeapon = WeaponManager.Instance.GetWeapon(WeaponType.BulletSizeUp) as BulletSizeUpWeapon;
		BulletSizeUpWeapon.ResetSize(this);
	}

	private void FixedUpdate()
	{
		_damageCaster.CastDamage(_damage);
	}

	public void OnPop()
	{
		StartCoroutine(CoroutineMove());
		OnResize();
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

	public void OnResize()
	{
		transform.localScale = DefaultSize * MultipliedCount;
	}
}
