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

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
		_massProductionDroneCompos = GetComponentsInChildren<IMassProductionDroneCompo>();
		foreach (IMassProductionDroneCompo compo in _massProductionDroneCompos)
		{
			compo.Init(this, (int)_level);
		}
	}

	private void Start()
	{
		_player = GameManager.Instance.Player;
	}	
}
