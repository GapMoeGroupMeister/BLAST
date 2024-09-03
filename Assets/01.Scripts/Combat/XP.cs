using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Crogen.ObjectPooling;

public class XP : MonoBehaviour, IPoolingObject
{
	[SerializeField] private float _radius;
    [SerializeField] private LayerMask _whatIsPlayer;
    private Collider[] _colliders;
	private bool _isMoving = false;

	//Managements
	private XPManager _xpManager;

	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	private void Awake()
	{
		_colliders = new Collider[1];
	}

	//Dissolve 넣을 예정!

	public void OnPop()
	{
		_xpManager ??= XPManager.Instance;
		_isMoving = false;
	}

	public void OnPush()
	{
		++_xpManager.XP;
	}

	private void FixedUpdate()
	{
		if (_isMoving == true) return;

		if (Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _whatIsPlayer) > 0)
		{
			transform.DOMove(_colliders[0].transform.position, 0.1f).OnComplete(()=> 
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
