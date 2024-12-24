using System;
using Crogen.ObjectPooling;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
	private static int _coin = 0;
	public static int GetCoin() => _coin;
	private static GameObject _dummyObject;
	public static Action<int> OnCoinCountChangedEvent;
	
	protected override void Awake()
	{
		base.Awake();
		_dummyObject = gameObject;
		_coin = 0;
	}

	public static void AddCoin(int value)
	{
		_coin += value;
	}	

	public static void SaveCoin()
	{
		GameDataManager.Instance.AddCoin(_coin);
		OnCoinCountChangedEvent?.Invoke(_coin);
	}

	public static void CreateCoin(Vector3 pos)
	{
		_dummyObject.Pop(PoolType.Coin, pos + Vector3.up, Quaternion.identity);
	}
}