using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using EffectSystem;

public abstract class DamageCaster : MonoBehaviour
{
    public bool excluded;
	public int allocationCount = 32;
	[SerializeField] protected LayerMask _whatIsCastable;
	protected Collider[] _castColliders;
	[SerializeField] private bool _usingExcludeCast = true;
    public List<DamageCaster> excludedDamageCasterList;

	public event Action OnCasterEvent;
	public event Action OnDamageCastSuccessEvent;

	[Header("DamageEffect")]
	[SerializeField] private EffectStateTypeEnum _effectStateType = 0;
	[SerializeField] private float _effectDuration = 0f;
	[SerializeField] private int _effectLevel = 1;

	protected Vector3 GetFinalCenter(Vector3 center)
	{
		Vector3 finalCenter;
		finalCenter.x = center.x * transform.lossyScale.x;
		finalCenter.y = center.y * transform.lossyScale.y;
		finalCenter.z = center.z * transform.lossyScale.z;
		finalCenter = transform.rotation * finalCenter;
		return finalCenter;
	}

	protected virtual void Awake()
	{
		_castColliders = new Collider[allocationCount];
	}

	public abstract void CastOverlap();

	public virtual void CastDamage(int damage)
	{
		CastOverlap();

		//����
		if(_usingExcludeCast)
			ExcludeCast(_castColliders);

		//������ ������
		for (int i = 0; i < _castColliders.Length; ++i)
		{
			if (_castColliders[i] == null)
			{
				break;
			}
			if (_castColliders[i].TryGetComponent(out Agent agent))
			{

				agent.AgentEffectController.ApplyEffect(_effectStateType, _effectDuration, _effectLevel);
				agent.HealthCompo.TakeDamage(damage);
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