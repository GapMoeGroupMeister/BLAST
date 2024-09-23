using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour, IPoolingObject
{
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	[SerializeField] private float _radius;
	[SerializeField] private LayerMask _whatIsPlayer;
	private Collider[] _colliders;
	private bool _isMoving = false;

	//Managements
	private ResourceManager _resourceManager;

	private void Awake()
	{
		_colliders = new Collider[1];
	}

	public void OnPop()
	{
		_resourceManager ??= ResourceManager.Instance;
		_isMoving = false;
	}

	public void OnPush()
	{
		_resourceManager.AddCoin(5);
	}

	private void FixedUpdate()
	{
		if (_isMoving == true) return;

		if (Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _whatIsPlayer) > 0)
		{
			transform.DOMove(_colliders[0].transform.position, 0.1f).OnComplete(() =>
			{
				this.Push();
			});
			_isMoving = true;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _radius);
		Gizmos.color = Color.white;
	}
}
