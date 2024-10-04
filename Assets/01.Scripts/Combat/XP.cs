using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Crogen.ObjectPooling;
using ItemManage;

public enum XPType
{
	Green,
	Yellow,
	Orange,
	Red
}

public class XP : Item, IPoolingObject
{
	[SerializeField] private float _radius;
    [SerializeField] private LayerMask _whatIsPlayer;
    private Collider[] _colliders;
	private bool _isMoving = false;

	private Material _material;

	private int _xpAmount;

	//Managements
	private XPManager _xpManager;
	private int _colorID;
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	private void Awake()
	{
		_colliders = new Collider[1];
		_material = transform.Find("Visual").GetComponent<MeshRenderer>().material;
		_colorID = Shader.PropertyToID("_Color");
	}

	public void SetGrade(XPType exType)
	{
		switch (exType)
		{
			case XPType.Green:
				_material.SetColor(_colorID, Color.green);
				_xpAmount = 1;
				break;
			case XPType.Yellow:
				_material.SetColor(_colorID, Color.yellow);
				_xpAmount = 2;
				break;
			case XPType.Orange:
				_material.SetColor(_colorID, new Color(1f, 0.5f, 0f));
				_xpAmount = 4;
				break;
			case XPType.Red:
				_material.SetColor(_colorID, Color.red);
				_xpAmount = 8;
				break;
		}
	}

	protected override void GetEffect()
	{
		this.Push();
	}

	public void OnPop()
	{
		_xpManager ??= XPManager.Instance;
		_isMoving = false;
	}

	public void OnPush()
	{
		_xpManager.XP += _xpAmount;
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
