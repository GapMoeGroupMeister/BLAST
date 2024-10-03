using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct EffectStatePair
{
	public EffectStatePair(float duration, int level, float percent = 1f)
	{
		this.duration = duration;
		this.level = level;
		this.percent = percent;
	}

	public float duration;
	public int level;

	/// <summary>
	/// percent : 0f ~ 1f
	/// </summary>
	public float percent;
}

public class EffectCaster : MonoBehaviour
{
	[Header("DamageCaster�� ���� ������")]
	private Dictionary<EffectStateTypeEnum, EffectStatePair> _effectStateTypeDictionary = new Dictionary<EffectStateTypeEnum, EffectStatePair>();

	public bool IsContainType(EffectStateTypeEnum effectStateType)
	{
		return _effectStateTypeDictionary.ContainsKey(effectStateType);
	}

	public void AddEffectState(EffectStateTypeEnum type, EffectStatePair effectStatePair)
	{
		if (_effectStateTypeDictionary.ContainsKey(type))
		{
			_effectStateTypeDictionary[type] = effectStatePair;
		}
		else
		{
			_effectStateTypeDictionary.Add(type, effectStatePair);
		}
	}

	/// <summary>
	/// EffectState�� �߰��մϴ�. ���� ������ EffectState�� �����Ѵٸ� ���� ����ϴ�.
	/// </summary>
	public void AddEffectState(EffectStateTypeEnum type, float duration, int level, float percent = 1f)
	{
		EffectStatePair effectStatePair = new EffectStatePair(duration, level, percent);

		if (_effectStateTypeDictionary.ContainsKey(type))
		{
			_effectStateTypeDictionary[type] = effectStatePair;
		}
		else
		{
			_effectStateTypeDictionary.Add(type, effectStatePair);
		}
	}

	public void RemoveEffectState(EffectStateTypeEnum type)
	{
		_effectStateTypeDictionary.Remove(type);
	}
	/// <summary>
	/// Ÿ�� �� NONE�� ������ �ش� ���� �ǳʶڴ�.
	/// </summary>
	public void TryApplyEffect(IEffectable effectable)
	{
		foreach (var elem in _effectStateTypeDictionary)
		{
			if (elem.Key == EffectStateTypeEnum.None) continue;
			effectable.ApplyEffect(
				elem.Key,
				elem.Value.duration,
				elem.Value.level,
				elem.Value.percent);
		}
	}
}
