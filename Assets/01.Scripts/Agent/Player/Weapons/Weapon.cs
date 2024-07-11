using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public PlayerPart basePart;
	public float maxAttackDelay = 10f;
	public float currentAttackDelay = 0f;

	[Header("Weapon")]
	public WeaponEffect weaponEffect;
	[SerializeField] private List<Transform> _weaponEffectSpawnPointList;

    public virtual void OnAttack()
	{
		if (currentAttackDelay < maxAttackDelay) return;
		currentAttackDelay = 0;

		for (int i = 0; i < _weaponEffectSpawnPointList.Count; ++i)
		{
			if(_weaponEffectSpawnPointList[i] != null)
				Instantiate(weaponEffect, _weaponEffectSpawnPointList[i].position, Quaternion.LookRotation(_weaponEffectSpawnPointList[i].forward));
		}
	}

	public virtual void Update()
	{
		if(currentAttackDelay < maxAttackDelay)
		{
			currentAttackDelay += Time.deltaTime;
		}
	}
}