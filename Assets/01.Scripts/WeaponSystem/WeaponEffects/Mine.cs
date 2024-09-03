using Crogen.ObjectPooling;
using UnityEngine;

public class Mine : WeaponEffect, IPoolingObject
{
	private DamageCaster _damageCaster;

	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
	}

	public void OnPop()
	{
		_damageCaster ??= GetComponent<DamageCaster>();
		_damageCaster.OnDamageCastSuccessEvent += this.Push;
	}

	public void OnPush()
	{
		_damageCaster.OnDamageCastSuccessEvent -= this.Push;
	}

	private void FixedUpdate()
	{
		if (_damageCaster == null) return;
		_damageCaster.CastDamage(_damage + (int)(_damage * (_level/10f)));
	}

}
