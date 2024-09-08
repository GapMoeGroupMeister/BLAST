using System;
using System.Collections;
using System.Collections.Generic;
using Crogen.ObjectPooling;
using UnityEngine;

public class TurretWeapon : Weapon
{
	[SerializeField] private PoolType _turretPoolType;
	[SerializeField] private int _turretMaxAmount = 10;
	[SerializeField] private float _enemyFindRadius = 20f;
	
	private int _turretAmount = 0;
	private List<Turret> _turrets = new List<Turret>();
	private Collider[] _colliders = new Collider[2];
	
	[ContextMenu("UseWeapon")]
	public override bool UseWeapon()
	{
		if(base.UseWeapon())
		{
			SpawnTurret();	
		}	

		return true;
	}

	private void SpawnTurret()
	{
		StartCoroutine(SpawnCoroutine());
	}

	private IEnumerator SpawnCoroutine()
	{
		if (_turretAmount >= _turretMaxAmount)
			yield break;
		

		var pos = GetCirclePosition(player.transform.position, 20f);
		
		for (int i = 0; i < level; i++)
		{
			_turrets.Add(SpawnTurretObj(pos[i]));
			yield return null;
		}

		yield return null;
	}

	private List<Vector3> GetCirclePosition(Vector3 transformPosition, float f)
	{
		List<Vector3> positions = new List<Vector3>();
		for (int i = 0; i < level; i++)
		{
			Vector3 pos = new Vector3(
				transformPosition.x + f * Mathf.Cos(i * 2 * Mathf.PI / level),
				transformPosition.y,
				transformPosition.z + f * Mathf.Sin(i * 2 * Mathf.PI / level));
			positions.Add(pos);
		}
		
		return positions;
	}
	
	private Turret SpawnTurretObj(Vector3 position)
	{
		Turret turret = gameObject.Pop(_turretPoolType, position, Quaternion.identity) as Turret;
		_turretAmount++;
		turret.Init(level, this);
		Debug.Log("Turret Spawned");
		turret.SetTarget(GetNearestEnemy(turret.transform.position));
		return turret;
	}
	
	private void OnDisable()
	{
		StopAllCoroutines();
	}
	
	private Transform GetNearestEnemy(Vector3 pos)
	{
		int count = Physics.OverlapSphereNonAlloc(pos, _enemyFindRadius, _colliders, whatIsEnemy);
		
		if (count == 0)
			return null;
		return _colliders[0].transform;

	}
	

	protected override void Update()
	{
		base.Update();
		if (_turrets.Count == 0) return;
		for (int i = 0; i < _turrets.Count; i++)
		{
			if (!_turrets[i].IsPushed) continue;
			_turretAmount--;
			_turrets.RemoveAt(i);
			i--;
		}
	}

	private void OnDrawGizmosSelected()
	{
		player ??= FindObjectOfType<Player>();
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(player.transform.position, _enemyFindRadius);
	}
}