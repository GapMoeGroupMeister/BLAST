using Crogen.CrogenPooling;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour, IPoolingObject
{
	public string OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }
	[SerializeField] private int _coinAmount = 15;
	[SerializeField] private float _radius;
	[SerializeField] private LayerMask _whatIsPlayer;
	private Collider[] _colliders;
	private bool _isMoving = false;


	private void Awake()
	{
		_colliders = new Collider[1];
	}

	public void OnPop()
	{
		_isMoving = false;
	}

	public void OnPush()
	{
		ResourceManager.AddCoin(_coinAmount);
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
