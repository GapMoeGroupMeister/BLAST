using UnityEngine;
using DG.Tweening;
using Crogen.CrogenPooling;
using System;

public class BallisticMissile : WeaponEffect
{
	private Transform _target;
	[SerializeField] private BallisticMissileSign _ballisticMissileSignPrefab;
	[SerializeField] private EffectPoolType _explosionPoolType;
	[SerializeField] private float _moveDuration = 1f;
	[SerializeField] private float _maxPosY = 150f;
	[SerializeField] private DamageCaster _damageCaster;

	[SerializeField] private FeedbackPlayer _startFeedbackPlayer;
	[SerializeField] private FeedbackPlayer _endFeedbackPlayer;

	public override void Init(uint level, Weapon weaponBase)
	{
		base.Init(level, weaponBase);
		_damage = 2;
	}

	public void SetTarget(Transform targetTrm)
	{
		_target = targetTrm;
		MoevUp();
	}

	//���� ���
	private void MoevUp()
	{
		_startFeedbackPlayer?.PlayFeedback();
		transform.DOMoveY(_maxPosY, _moveDuration).OnComplete(AttackTarget).SetEase(Ease.InCubic);
	}

	//Ÿ�� ����
	private void AttackTarget()
	{
		transform.SetParent(_target);
		transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
		ShowTargetSign(MoveDown);
	}

	//������ �����ֱ�
	private void ShowTargetSign(Action EndEvent)
	{
		BallisticMissileSign ballisticMissileSign = Instantiate(_ballisticMissileSignPrefab, _target);
		ballisticMissileSign.OnEndEvent += EndEvent;
		ballisticMissileSign.OnInGauge();
	}

	//��������
	private void MoveDown()
	{
		transform.forward = Vector3.down;
		transform.DOLocalMoveY(0, _moveDuration).OnComplete(ExplosionEffect).SetEase(Ease.InCubic);
	}

	//�����ϱ�
	private void ExplosionEffect()
	{
		_endFeedbackPlayer?.PlayFeedback();
		_damageCaster.CastDamage(_damage);
		gameObject.Pop(_explosionPoolType, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}