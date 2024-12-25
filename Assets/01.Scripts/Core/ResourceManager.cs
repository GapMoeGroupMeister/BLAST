using System;
using Crogen.ObjectPooling;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	private static int _coin = 0;
	public static int GetCoin() => _coin;
	private static GameObject _dummyObject;
	public static Action<int> OnCoinCountChangedEvent;
	
	private void Awake()
	{
		_dummyObject = gameObject;
		_coin = 0;
	}

	public static void AddCoin(int value)
	{
		_coin += value;
		OnCoinCountChangedEvent?.Invoke(_coin);
	}	

	public static void SaveCoin()
	{
		GameDataManager.Instance.AddCoin(_coin);
	}

	public static void CreateCoin(Vector3 pos)
	{
		_dummyObject.Pop(PoolType.Coin, pos + Vector3.up, Quaternion.identity);
	}
}