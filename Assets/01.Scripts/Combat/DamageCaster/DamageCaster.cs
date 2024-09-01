using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class DamageCaster : MonoBehaviour
{
    public bool excluded;
	protected LayerMask _whatIsCastable;
	[HideInInspector] public Collider[] castColliders;
    public List<DamageCaster> excludedDamageCasterList;

	protected virtual void Awake()
	{
		castColliders = new Collider[128];
	}

	public virtual void CastOverlap()
	{
		castColliders = castColliders.Where(x => x != null).ToArray();
	}

	public virtual void CastDamage(int damage)
	{
		CastOverlap();

		//제외
		ExcludeCast(castColliders);

		//데미지 입히기
		for (int i = 0; i < castColliders.Length; ++i)
		{
			if (castColliders[i].TryGetComponent(out Health health))
			{
				health.TakeDamage(damage);
			}
		}

		castColliders = new Collider[128];
	}

	protected void ExcludeCast(Collider[] colliders)
	{
		foreach (var excludeCaster in excludedDamageCasterList)
		{
			excludeCaster.CastOverlap();
			colliders = colliders.Except(excludeCaster.castColliders).ToArray();
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