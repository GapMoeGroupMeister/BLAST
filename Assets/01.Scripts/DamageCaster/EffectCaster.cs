using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectCaster : MonoBehaviour
{
    [Header("DamageCaster랑 같이 쓰세요")]
	//Dictionary로 바꾸기 꼭!!!!!!!!!!!!!!!!!!!!
    [SerializeField] private List<EffectStatePair> _effectStateTypeList;

	public bool IsContainType(EffectStateTypeEnum effectStateType)
	{
		return _effectStateTypeList.Any(x => x.effectStateType == effectStateType);
	}

	public void AddEffectState(EffectStatePair effectStatePair)
	{
		_effectStateTypeList.Add(effectStatePair);
	}

	public void AddEffectState(EffectStateTypeEnum effectStateType, float duration, int level, float percent = 1f)
	{
		EffectStatePair effectStatePair = new EffectStatePair(effectStateType, duration, level, percent);

		_effectStateTypeList.Add(effectStatePair);
	}

	public void RemoveEffectState(EffectStateTypeEnum effectStateType)
	{
		_effectStateTypeList.Remove(_effectStateTypeList.Find(x => x.effectStateType == effectStateType));
	}

	public void TryApplyEffect(IEffectable effectable)
	{
		for (int i = 0; i < _effectStateTypeList.Count; ++i)
		{
			if (_effectStateTypeList[i].effectStateType == EffectStateTypeEnum.None) continue;
			effectable.ApplyEffect(
				_effectStateTypeList[i].effectStateType,
				_effectStateTypeList[i].duration,
				_effectStateTypeList[i].level,
				_effectStateTypeList[i].percent);
		}
	}
}
