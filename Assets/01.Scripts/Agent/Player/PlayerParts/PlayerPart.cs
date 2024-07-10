using System;
using System.Collections;
using Crogen.PowerfulInput;
using UnityEngine;
using Crogen.ObjectPooling;

public delegate void PlayerOverloadEvent(int curOverload, int maxOverload);

[Serializable]
public class MagazineInfo
{
	public int curOverload;
	public int maxOverload = 20;
	public event PlayerOverloadEvent playerOverloadEvent;

	public float attackDelay = 0.2f;
	[HideInInspector] public float curAttackDelay;
	
	public Action<Vector3> OnAttackEvent;
	[SerializeField] public Transform[] bulletFirePositions;

	[HideInInspector] public Vector3 AttackDirection;

	public bool IsAttack { get; private set; }

	[SerializeField] private PoolType _bulletPoolingType;
	
	public void HandleAttackUpdate(bool value)
	{
		IsAttack = value;
	}
	
	public void Attack()
	{
		if (curAttackDelay > 0 || curOverload >= maxOverload) return;

		curAttackDelay = attackDelay;
		++curOverload;
		OnAttackEvent?.Invoke(AttackDirection);
		playerOverloadEvent?.Invoke(curOverload, maxOverload);
		for (int i = 0; i < bulletFirePositions.Length; ++i)
		{
			bulletFirePositions[i].gameObject.Pop(
				_bulletPoolingType,
				bulletFirePositions[i].position, 
				Quaternion.LookRotation(AttackDirection));
		}
	}

	public void SetDelay()
	{
		if (attackDelay > 0)
		{
			curAttackDelay -= Time.deltaTime;
			if (curAttackDelay <= 0)
			{
				curAttackDelay = 0;
			}
		}
	}
	
	public void SetOverload()
	{
		if (curOverload < maxOverload)
		{
			--curOverload;
		}
	}
}

public abstract class PlayerPart : MonoBehaviour
{
	private InputReader _inputReader;
	protected PlayerPartController _playerPartController;
    public PlayerPartType playerPartType;

	[Header("MagazineL")] 
	public MagazineInfo magazineInfoL;
	
	[Header("MagazineR")] 
	public MagazineInfo magazineInfoR;

	private PlayerMovement _playerMovement;
	public LayerMask whatIsEnemy;

	protected virtual void OnEnable()
	{
		_playerPartController = PlayerPartController.Instance;
		_inputReader = GameManager.Instance.InputReader;
		_playerMovement = GameManager.Instance.Player.MovementCompo as PlayerMovement;
		
		_inputReader.AttackLEvent += magazineInfoL.HandleAttackUpdate;
		_inputReader.AttackREvent += magazineInfoR.HandleAttackUpdate;
		StartCoroutine(CoroutineUpdateOverload());
	}

	private void OnDestroy()
	{
		_inputReader.AttackLEvent -= magazineInfoL.HandleAttackUpdate;
		_inputReader.AttackREvent -= magazineInfoR.HandleAttackUpdate;

		StopAllCoroutines();
	}

	protected virtual void Update()
	{
		if (magazineInfoL.IsAttack)
			magazineInfoL.Attack();
		
		if (magazineInfoR.IsAttack)
			magazineInfoR.Attack();
		
		magazineInfoL.SetDelay();
		magazineInfoR.SetDelay();
	}
	
	protected void FixedUpdate()
	{
		magazineInfoL.AttackDirection = magazineInfoR.AttackDirection = transform.rotation * Vector3.forward;
	}

	private IEnumerator CoroutineUpdateOverload()
	{
		while(true)
		{
			yield return new WaitForSeconds(1);
			magazineInfoL.SetOverload();
			magazineInfoR.SetOverload();
		}
	}
}
