using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Crogen.CrogenPooling;

public class SimplePoolingObject : MonoBehaviour, IPoolingObject
{
	string IPoolingObject.OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	[Header("Life")]
	public bool isAutoPush = true;
	public float duration;
	private float _curTime = 2f;

	[Header("Events")]
	public UnityEvent popEvent;
	public UnityEvent pushEvent;

	public void OnPop()
	{
		popEvent?.Invoke();
	}

	public void OnPush()
	{
		_curTime = 0f;
		pushEvent?.Invoke();
	}

	private void Update()
	{
		_curTime += Time.deltaTime;
		if(_curTime > duration)
		{
			this.Push();
		}
	}
}
