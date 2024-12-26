using Crogen.CrogenPooling;
using UnityEngine;
using System.Collections;

public class Mine : WeaponEffect, IPoolingObject
{
	public string OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	[SerializeField] private DamageCaster _mainDamageCaster;
	[SerializeField] private DamageCaster _subDamageCaster;
	//[SerializeField] private PoolType _explosionPoolType = PoolType.BlueExplosion;
	private FeedbackPlayer feedbackPlayer;

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
	}

	private void Awake()
	{
		feedbackPlayer = GetComponentInChildren<FeedbackPlayer>();
	}

	public void OnPop()
	{
		_mainDamageCaster.OnDamageCastSuccessEvent += OnExplosion;
		StartCoroutine(CoroutineAutoDie());
	}

	public void OnPush()
	{
		_mainDamageCaster.OnDamageCastSuccessEvent -= OnExplosion;
		feedbackPlayer.PlayFeedback();
		StopAllCoroutines();
	}

	private void FixedUpdate()
	{
		_mainDamageCaster.CastDamage(_damage + (int)(_damage * ((_level-1)/10f)));
	}

	private void OnExplosion()
	{
		// _subDamageCaster.CastDamage((int)((_damage + (_damage * ((_level - 1) / 10f))) * 0.6f));
		// gameObject.Pop(_explosionPoolType,transform.position, Quaternion.identity);
		// this.Push();
	}

	private IEnumerator CoroutineAutoDie()
	{
		yield return new WaitForSeconds(30f);
		OnExplosion();
	}
}
