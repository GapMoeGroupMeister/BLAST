using System;
using System.Collections;
using Crogen.PowerfulInput;
using UnityEngine;
using Crogen.ObjectPooling;

public delegate void PlayerOverloadEvent(int curOverload, int maxOverload);

[Serializable]
public class MagazineInfo
{
	[Header("Overload")]
	public int curOverload;
	public int maxOverload = 20;
	public float overloadDelay = 0.5f;
	public event PlayerOverloadEvent playerOverloadEvent;

	[Header("Attack")]
	public float attackDelay = 0.2f;
	[HideInInspector] public float curAttackDelay;
	
	public Action<Vector3> OnAttackEvent;
	[SerializeField] public Transform[] bulletFirePositions;

	[HideInInspector] public Vector3 AttackDirection;

	public bool IsAttack { get; private set; }

	[SerializeField] private PoolType _bulletPoolingType;
	
	public void HandleAttackUpdate(bool value)
	{
		if (curOverload <= maxOverload)
			IsAttack = value;
	}
	
	public void Attack()
	{
		if (curAttackDelay > 0 || curOverload >= maxOverload) return;

		curAttackDelay = attackDelay;
		++curOverload;
		playerOverloadEvent?.Invoke(curOverload, maxOverload);
		OnAttackEvent?.Invoke(AttackDirection);
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
		if (curOverload > 0 && IsAttack == false)
		{
			--curOverload;
			playerOverloadEvent?.Invoke(curOverload, maxOverload);
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
		float currentTimeL=0;
		float currentTimeR=0;

		while(true)
		{
			yield return null;
			currentTimeL += Time.deltaTime;
			currentTimeR += Time.deltaTime;

			if(currentTimeL > magazineInfoL.overloadDelay)
			{
				magazineInfoL.SetOverload();
				currentTimeL = 0;
			}
			if(currentTimeR > magazineInfoR.overloadDelay)
			{
				magazineInfoR.SetOverload();
				currentTimeR = 0;
			}
		}
	}
}
