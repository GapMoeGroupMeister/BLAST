using Crogen.ObjectPooling;
using UnityEngine;
using System.Collections;

public class Mine : WeaponEffect, IPoolingObject
{
	public PoolType OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	[SerializeField] private DamageCaster _mainDamageCaster;
	[SerializeField] private DamageCaster _subDamageCaster;
	[SerializeField] private PoolType _explosionPoolType = PoolType.BlueExplosion;

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
	}

	public void OnPop()
	{
		_mainDamageCaster.OnDamageCastSuccessEvent += OnExplosion;
		StartCoroutine(CoroutineAutoDie());
	}

	public void OnPush()
	{
		_mainDamageCaster.OnDamageCastSuccessEvent -= OnExplosion;
		StopAllCoroutines();
	}

	private void FixedUpdate()
	{
		_mainDamageCaster.CastDamage(_damage + (int)(_damage * ((_level-1)/10f)));
	}

	private void OnExplosion()
	{
		_subDamageCaster.CastDamage((int)((_damage + (_damage * ((_level - 1) / 10f))) * 0.6f));
		gameObject.Pop(_explosionPoolType,transform.position, Quaternion.identity);
		this.Push();
	}

	private IEnumerator CoroutineAutoDie()
	{
		yield return new WaitForSeconds(30f);
		OnExplosion();
	}
}
