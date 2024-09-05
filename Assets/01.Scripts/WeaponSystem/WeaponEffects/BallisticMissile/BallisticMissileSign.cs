using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class BallisticMissileSign : MonoBehaviour
{
	public event Action OnEndEvent;
	[SerializeField] private DecalProjector _decalRenderer;
	private Material _mat;
	private int _valueID;

	private void Awake()
	{
		_valueID = Shader.PropertyToID("_Value");
		_mat = _decalRenderer.material;
	}

	public void OnInGauge()
	{
		_mat.SetFloat(_valueID, 0);
		DOTween.To(() => _mat.GetFloat(_valueID), x => _mat.SetFloat(_valueID, x), 1f, 1f).OnComplete(()=>
		{
			OnEndEvent?.Invoke();
			Destroy(gameObject);
		}); 
	}
}
