using DG.Tweening;
using UnityEngine;
using Crogen.ObjectPooling;

public class Bullet : MonoBehaviour, IPoolingObject
{
	public float speed = 100f;
	public float duration = 5f;
	public LayerMask _whatIsEnemy;
	private Collider[] _colliders;

	[SerializeField] private float _collisionRadius = 1f;
	[SerializeField] private Vector3 _offset;

	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	private void Awake()
	{
		_colliders = new Collider[10];
		
	}

	private void FixedUpdate()
	{
		if (Physics.OverlapSphereNonAlloc(transform.position + _offset, _collisionRadius, _colliders, _whatIsEnemy) > 0)
		{
			transform.DOKill();
			this.Push();
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position + _offset, _collisionRadius);
	}

	public void OnPop()
	{
		transform.DOMove(transform.forward * speed * duration, duration).OnComplete(() => { this.Push(); });
	}

	public void OnPush()
	{
	}
}
