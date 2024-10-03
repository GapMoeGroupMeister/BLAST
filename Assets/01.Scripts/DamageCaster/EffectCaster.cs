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
	[Header("DamageCaster랑 같이 쓰세요")]
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
	/// EffectState를 추가합니다. 만약 기존에 EffectState가 존재한다면 값을 덮어씁니다.
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
	/// 타입 중 NONE이 있으면 해당 값을 건너뛴다.
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
