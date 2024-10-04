using Crogen.ObjectPooling;
using System;
using TMPro;
using UnityEngine;

public class XPManager : MonoSingleton<XPManager>
{
	[SerializeField] private PoolType _xpPoolType;
	[SerializeField] private TextMeshProUGUI _lvText;
	public event Action<float> OnXPPercentEvent;
	public event Action<int> OnLevelUpEvent;

	private int _level = 1;
	[SerializeField] private int _maxXP = 20;
	[SerializeField] private int _xp;

	public int GetLevel => _level;

	public int XP
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
				_lvText.text = $"lv.{_level.ToString("00")}";
			}

			//이벤트 실행
			OnXPPercentEvent?.Invoke((float)_xp / _maxXP);
		}
	}

	//나중에 밸런싱!
	private void MaxXPUp()
	{
		_maxXP = (int)(_maxXP * 1.25f);
	}

	public void CreateXP(Vector3 pos, XPType xpType)
	{
		XP xp = gameObject.Pop(_xpPoolType, pos + Vector3.up, Quaternion.identity) as XP;
		xp.SetGrade(xpType);
	}
}
