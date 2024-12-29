using Crogen.CrogenPooling;
using System;
using UnityEngine;

public class XPManager : MonoBehaviour
{
	public static event Action<float> OnXPPercentEvent;
	public static event Action<int> OnLevelUpEvent;

	private static int _level = 1;
	[SerializeField] private static int _maxXP = 5;
	[SerializeField] private static int _xp;

	public static int GetLevel => _level;
	private static GameObject _dummyObject;

	private void Awake()
	{
		_dummyObject = gameObject;
		_xp = 0;
		_level = 1;
		_maxXP = 5;
	}

	public static int XP
	{
		get => _xp;
		set
		{
			//경험치 최대치 갱신
			if(_xp >= _maxXP)
			{
				_xp = 0;
				++_level;
				OnLevelUpEvent?.Invoke(_level);
				MaxXPUp();
			}
			else
			{
				_xp = value;
			}

			OnXPPercentEvent?.Invoke((float)_xp / _maxXP);
		}
	}

	//나중에 밸런싱!
	private static void MaxXPUp()
	{
		_maxXP = (int)(_maxXP * 1.37f);
	}

	public static void CreateXP(Vector3 pos, XPType xpType)
	{
		XP xp = _dummyObject.gameObject.Pop(OtherPoolType.XP, pos + Vector3.up, Quaternion.identity) as XP;
		xp.SetGrade(xpType);
	}

}
