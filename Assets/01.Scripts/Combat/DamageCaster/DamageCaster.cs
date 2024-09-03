using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class DamageCaster : MonoBehaviour
{
    public bool excluded;
	public int allocationCount = 32;
	[SerializeField] protected LayerMask _whatIsCastable;
	protected Collider[] _castColliders;
    public List<DamageCaster> excludedDamageCasterList;
	public event Action OnCasterEvent;
	public event Action OnDamageCastSuccessEvent;
	
	protected virtual void Awake()
	{
		_castColliders = new Collider[allocationCount];
	}

	public abstract void CastOverlap();

	public virtual void CastDamage(int damage)
	{
		CastOverlap();

		//제외
		ExcludeCast(_castColliders);

		//데미지 입히기
		for (int i = 0; i < _castColliders.Length; ++i)
		{
			if (_castColliders[i] == null)
			{
				break;
			}
			if (_castColliders[i].TryGetComponent(out Health health))
			{
				health.TakeDamage(damage);
				OnDamageCastSuccessEvent?.Invoke();
			}
		}

		OnCasterEvent?.Invoke();
	}

	protected void ExcludeCast(Collider[] colliders)
	{
		foreach (var excludeCaster in excludedDamageCasterList)
		{
			excludeCaster.CastOverlap();
			colliders = colliders.Except(excludeCaster._castColliders).ToArray();
		}
	}

	private void OnValidate()
	{
		if (excludedDamageCasterList == null) return;
		for (int i = 0; i < excludedDamageCasterList.Count; ++i)
		{
			if (excludedDamageCasterList[i] == null) continue;

			if (excludedDamageCasterList[i].excluded == false)
				excludedDamageCasterList[i] = null;
		}
	}
}