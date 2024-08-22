using UnityEngine;
using DG.Tweening;

public class Missile : WeaponEffect
{
	[SerializeField] private int _damage=1;
	[SerializeField] private float _lifeTime = 5f;
	[SerializeField] private float _speed = 50f;

	protected override void OnEnable()
	{
		base.OnEnable();
		Vector3 originPos = transform.position;
		transform.DOMove(originPos + transform.forward.normalized * _speed * _lifeTime, _lifeTime);
	}

	protected override void OnAttack()
	{
		for (int i = 0; i < targets.Length; ++i)
		{
			if(targets[i].TryGetComponent(out Health health))
			{
				health.TakeDamage(_damage);
			}
		}
		Destroy(gameObject);
	}
}
