using Crogen.CrogenPooling;
using UnityEngine;

public class TrailRendererPoolingExtension : MonoBehaviour
{
	[SerializeField] private Transform _poolingObjectParent;
	public float duration = 2f;
	private float _curTime = 0f;
	private TrailRenderer _trailRenderer;
	private IPoolingObject _owner;
	private bool _isParentDisable = false;

	private void Awake()
	{
		_owner = _poolingObjectParent.GetComponent<IPoolingObject>();
		_trailRenderer = GetComponent<TrailRenderer>();

		if (_trailRenderer == null)
		{
			Debug.LogError("TrailRenderer가 존재하지 않습니다.");
			return;
		}

		if (_owner == null)
			Debug.LogError("TrailRenderer에게 IPoolingObject를 상속받는 부모가 존재하지 않습니다.");
	}

	private void OnDisable()
	{
		_trailRenderer.Clear();
		_isParentDisable = false;
		_curTime = 0f;
	}

	private void Update()
	{
		Debug.Log(_poolingObjectParent.gameObject.activeInHierarchy);
		if (_poolingObjectParent.gameObject.activeInHierarchy == false && _isParentDisable == false)
		{
			Debug.Log("nrtnrents");
			transform.SetParent(null, true);
			_isParentDisable = true;
		}

		if (_isParentDisable)
		{
			_curTime += Time.deltaTime;

			if (_curTime > duration)
			{
				transform.SetParent(_poolingObjectParent.transform, false);
				Debug.Log("dfdf");
			}
		}
	}
}
