using System;
using UnityEngine;

public enum CompareMode
{
	Greater,
	Equals,
	NotEqual,
	Less
}

public class AutoWeapon : Weapon
{
	[Header("Auto Option")]
	[SerializeField] private ChangableValueEnum _targetValue; 
	[SerializeField] private CompareMode _compareMode;
	[SerializeField] private float _comparedValue;

	private void Start()
	{
		ChangableValueObserver.Instance.valueChangedDictionary[_targetValue].ValueChangedEvent += HandleValueChanged;
	}

	private void HandleValueChanged(float value)
	{
		if (!isCanAttack) return;

		switch (_compareMode)
		{
			case CompareMode.Greater:
				if(_comparedValue < value)
					OnAttack();
				break;
			case CompareMode.Equals:
				if (Mathf.Approximately(_comparedValue, value))
					OnAttack();
				break;
			case CompareMode.NotEqual:
				if (!Mathf.Approximately(_comparedValue, value))
					OnAttack();
				break;
			case CompareMode.Less:
				if (_comparedValue > value)
					OnAttack();
				break;
		}
	}
}
