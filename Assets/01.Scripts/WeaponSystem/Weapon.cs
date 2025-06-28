using System;
using Crogen.AttributeExtension;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Weapon : MonoBehaviour
{
	public bool canUse = true;

	[Header("Weapon이 활성화되었는가?")]
	public bool weaponEnabled;
	
	[FormerlySerializedAs("useCooldawn")] [FormerlySerializedAs("isConditionalWeapon")] [Header("스킬 실행")]
	public bool useCooldown;
	[SerializeField] protected float _cooldown;
	protected float _curCooldown;

	[Header("레벨")]
	[Range(1, 10)]
	public uint level=1;

	[Header("전용무기?")]
	[Tooltip("체크하면 이 무기는 전용무기가 됩니다.")]
	public bool isUniqueWeapon;
	[HideInInspectorByCondition(nameof(isUniqueWeapon))]
	public PlayerPartType partType;

	[HideInInspector] public Player player;
	public event CooldownInfoEvent OnCooldownEvent;
	public event Action<float> OnWeaponUseEvent;
	[Header("적이 뭐임?")]
	public LayerMask whatIsEnemy;

	protected virtual void Update()
	{
		if (_cooldown <= 0) return;
		
		if (_curCooldown > 0)
		{
			_curCooldown -= Time.deltaTime;
			OnCooldownEvent?.Invoke(_curCooldown, _cooldown);
		}
	}

	public virtual void WeaponInit() { }

	private float valueCheckPoint = 0;

	public virtual bool UseWeapon()
	{
		if (_curCooldown > 0 || weaponEnabled == false) return false;

		_curCooldown = _cooldown;
		OnWeaponUseEvent?.Invoke(level);
		return true;
	}
}