using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Crogen.ObjectPooling;

public class SimplePoolingObject : MonoBehaviour, IPoolingObject
{
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	[Header("Life")]
	public bool isAutoPush = true;
	public float lifeTime;

	[Header("Events")]
	public UnityEvent popEvent;
	public UnityEvent pushEvent;

	public void OnPop()
	{
		popEvent?.Invoke();
		StartCoroutine(CoroutineDie());
	}

	public void OnPush()
	{
		pushEvent?.Invoke();
	}

	IEnumerator CoroutineDie()
	{
		yield return new WaitForSeconds(lifeTime);
		this.Push();
	}
}
