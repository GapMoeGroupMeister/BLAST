using Crogen.ObjectPooling;
using System;
using UnityEngine;

public class XPManager : MonoSingleton<XPManager>
{
	[SerializeField] private PoolType _xpPoolType;

	public event Action<float> OnXPPercentEvent;
	public event Action<int> OnLevelUpEvent;

	private int _level = 1;
	private int _maxXP = 20;
	[SerializeField] private int _xp;

	public int GetLevel => _level;

	public int XP
	{
		get => _xp;
		set
		{
			//이벤트 실행
			OnXPPercentEvent?.Invoke(_xp / _maxXP);

			//경험치 최대치 갱신

			if(_xp >= _maxXP)
			{
				++_level;
				OnLevelUpEvent?.Invoke(_level);
				MaxXPUp();
				_xp = 0;
			}
			_xp = value;
		}
	}

	//나중에 밸런싱!
	private void MaxXPUp()
	{
		_maxXP = (int)(_maxXP * 1.75f);
	}

	public void CreateXP(Vector3 pos)
	{
		gameObject.Pop(_xpPoolType, pos + Vector3.up, Quaternion.identity);
	}
}
