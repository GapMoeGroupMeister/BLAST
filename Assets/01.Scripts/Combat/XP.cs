using UnityEngine;
using DG.Tweening;
using ItemManage;
using Crogen.CrogenPooling;

public enum XPType
{
	Green,
	Yellow,
	Orange,
	Red
}

public class XP : Item
{
	[SerializeField] private float _radius;
    [SerializeField] private LayerMask _whatIsPlayer;
    private Collider[] _colliders;
	private bool _isMoving = false;

	private Material _material;

	private int _xpAmount;

	private int _colorID;

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

	public override void OnPop()
	{
		_isMoving = false;
	}

	public override void OnPush()
	{
		XPManager.XP += _xpAmount;
	}

	private void FixedUpdate()
	{
		if (_isMoving == true) return;

		if (Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _whatIsPlayer) > 0)
		{
			MoveTo(_colliders[0].transform.position);
		}
	}

	public void MoveTo(Vector3 target)
	{
		float distance = Vector3.Distance(transform.position, target);
		transform.DOMove(target, distance/_radius * 0.03f).OnComplete(this.Push);
		_isMoving = true;
	}
	

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _radius);
		Gizmos.color = Color.white;
	}
}
