using Crogen.ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
	[SerializeField] private TextMeshProUGUI _goldText;
	private int _coin = 0;
	[SerializeField] private PoolType _coinPoolType = PoolType.Coin;

	public int GetCoin() => _coin;

	private void Awake()
	{
		_goldText.text = "0";
	}

	public void AddCoin(int value)
	{
		_coin += value;
		_goldText.text = $"{_coin}";
	}	

	public void SaveCoin()
	{
		GameDataManager.Instance.AddCoin(_coin);
	}

	public void CreateCoin(Vector3 pos)
	{
		gameObject.Pop(_coinPoolType, pos + Vector3.up, Quaternion.identity);
	}
}