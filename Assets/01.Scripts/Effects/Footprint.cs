using UnityEngine;
using DG.Tweening;
using Crogen.ObjectPooling;

public class Footprint : MonoBehaviour, IPoolingObject
{
	[SerializeField] private Material _decalMat;
	[SerializeField] private float _lifeTime = 3f;

	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }
	private int _alphaID;

	private void Awake()
	{
		_alphaID = Shader.PropertyToID("_Alpha");
	}

	public void OnPop()
	{
		_decalMat.DOFloat(0f, _alphaID, _lifeTime).SetEase(Ease.InCubic).OnComplete(()=> 
		{
			this.Push();
		});
	}

	public void OnPush()
	{
		_decalMat.SetFloat(_alphaID, 1f);
	}
}
