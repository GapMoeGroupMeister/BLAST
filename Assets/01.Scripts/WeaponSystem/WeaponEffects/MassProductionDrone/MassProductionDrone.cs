using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MassProductionDrone : WeaponEffect
{
	private Player _player;
	public Transform currentTarget;

	//Components
	private IMassProductionDroneCompo[] _massProductionDroneCompos;
	public bool isAttacking;
	private void Awake()
	{
		_massProductionDroneCompos = GetComponentsInChildren<IMassProductionDroneCompo>();
		Init();
	}

	private void Init()
	{
		foreach (IMassProductionDroneCompo compo in _massProductionDroneCompos)
		{
			compo.Init(this, (int)_level);
		}
	}

	private void Start()
	{
		_player = GameManager.Instance.Player;
		_weaponBase = WeaponManager.Instance.GetWeapon(WeaponType.MassProductionDrone) as MassProductionDroneWeapon;
	}	
}
