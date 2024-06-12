using ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class Bullet : PoolableMono
{
	public float speed = 100f;
	public float duration = 5f;
	public LayerMask _whatIsEnemy;
	private Collider[] _colliders;

	[SerializeField] private float _collisionRadius = 1f;

	private void Awake()
	{
		_colliders = new Collider[10];
	}

	public override void ResetItem()
	{
		transform.DOMove(transform.forward * speed * duration, duration).OnComplete(() => { PoolingManager.Instance.Push(this, true); });
	}

	private void FixedUpdate()
	{
		if (Physics.OverlapSphereNonAlloc(transform.position, _collisionRadius, _colliders, _whatIsEnemy) > 0)
		{
			transform.DOKill();
			PoolingManager.Instance.Push(this, true);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, _collisionRadius);
	}
}
