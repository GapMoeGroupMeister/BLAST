using System;
using UnityEngine;

public enum CompareMode
{
	Greater,
	Equals,
	NotEqual,
	Less
}

public enum AutoUseType
{
	ValueCondition,
	ValueCheck,
	Event
}

public abstract class Weapon : MonoBehaviour
{
	public bool canUse = true;

	[Header("Weapon이 활성화되었는가?")]
	public bool weaponEnabled;

	[Header("쿨다운")]
	[SerializeField] protected float _cooldown;
	protected float _curCooldown;

	[Header("레벨")]
	[Range(1, 10)]
	public uint level=1;

	[Header("전용무기인가?")]
	[Tooltip("체크하면 이 무기는 전용무기가 됩니다.")]
	public bool isUniqueWeapon;
	public PlayerPartType partType;

	[Header("조건부로 발동하는 무기인가?")]
	[Tooltip("체크하면 쿨다운뿐만 아니라 조건부까지 같이 고려하여 무기를 사용합니다.")]
	public bool isConditionalWeapon;

	[Header("조건부")]
	[SerializeField] protected AutoUseType _autoUseType;
	[SerializeField] protected CompareMode _compareMode;
	[SerializeField] protected float _targetValue;

	[HideInInspector] public Player player;
	public event CooldownInfoEvent OnCooldownEvent;
	public event Action<float> OnWeaponUseEvent;
	[Header("적이 뭐임?")]
	public LayerMask whatIsEnemy;

	protected virtual void Update()
	{
		if (_cooldown > 0)
		{
			_curCooldown -= Time.deltaTime;
			if (_curCooldown <= 0)
			{
				_curCooldown = 0;
			}
			OnCooldownEvent?.Invoke(_curCooldown, _cooldown);
		}
	}

	public virtual void WeaponInit() { }

	public void AutoUseWeaponByValueConditional(float newValue)
	{
		if (isConditionalWeapon)
		{
			if (_autoUseType != AutoUseType.ValueCondition) return;
			switch (_compareMode)
			{
				case CompareMode.Greater:
					if (newValue > this._targetValue)
						UseWeapon();
					return;
				case CompareMode.Equals:
					if (Mathf.Approximately(newValue, this._targetValue))
						UseWeapon();
					return;
				case CompareMode.NotEqual:
					if (Mathf.Approximately(newValue, this._targetValue) == false)
						UseWeapon();
					return;
				case CompareMode.Less:
					if (newValue < this._targetValue)
						UseWeapon();
					return;
				default:
					return;
			}
		}
		return;
	}

	private float valueCheckPoint = 0;

	public void AutoUseWeaponByValueCheck(float newValue)
	{
		if (isConditionalWeapon)
		{
			if (_autoUseType != AutoUseType.ValueCheck) return;
			if (newValue - valueCheckPoint > this._targetValue)
			{
				valueCheckPoint = newValue;
				UseWeapon();
			}
		}
		return;
	}

	public void AutoUseWeaponByEvent()
	{
		if (isConditionalWeapon)
		{
			if (_autoUseType != AutoUseType.Event) return;
			UseWeapon();
		}
		return;
	}

	public virtual bool UseWeapon()
	{
		if (_curCooldown > 0 || weaponEnabled == false) return false;

		_curCooldown = _cooldown;
		OnWeaponUseEvent?.Invoke(level);
		return true;
	}
}