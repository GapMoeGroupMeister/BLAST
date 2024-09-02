using UnityEngine;

public enum CompareMode
{
	Greater,
	Equals,
	NotEqual,
	Less
}

public abstract class Weapon : MonoBehaviour
{
	[Header("Weapon이 활성화되었는가?")]
	public bool weaponEnabled;

	[Header("쿨다운")]
	[SerializeField] protected float _cooldown;
	protected float _curCooldown;

	[Header("레벨")]
	public uint level=1;

	[Header("조건부로 발동하는 무기인가?")]
	[Tooltip("체크하면 쿨다운뿐만 아니라 조건부까지 같이 고려하여 무기를 사용합니다.")]
	public bool isConditionalWeapon;

	[Header("조건부")]
	[SerializeField] protected CompareMode _compareMode;
	[SerializeField] protected float _targetValue;

	[HideInInspector] public Player player;
	public event CooldownInfoEvent OnCooldownEvent;
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

	public virtual bool UseWeapon(float value)
	{
		if (_curCooldown > 0 || weaponEnabled == false) return false;

		if(isConditionalWeapon)
		{
			switch (_compareMode)
			{
				case CompareMode.Greater:
					if (value < this._targetValue)
						return true;
					break;
				case CompareMode.Equals:
					if (Mathf.Approximately(value, this._targetValue))
						return true;
					break;
				case CompareMode.NotEqual:
					if (Mathf.Approximately(value, this._targetValue) == false)
						return true;
					break;
				case CompareMode.Less:
					if (value > this._targetValue)
						return true;
					break;
				default:
					return false;
			}
		}

		_curCooldown = _cooldown;
		return true;
	}

	public virtual bool UseWeapon()
	{
		if (_curCooldown > 0 || weaponEnabled == false) return false;

		_curCooldown = _cooldown;
		return true;
	}
}