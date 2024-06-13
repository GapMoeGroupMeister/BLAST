using System;
using Crogen.PowerfulInput;
using ObjectPooling;
using UnityEngine;

public delegate void PlayerMagazineEvent(int curMagazine, int maxMagazine);

[Serializable]
public class MagazineInfo
{
	public int curMagazine;
	public int maxMagazine = 20;
	public event PlayerMagazineEvent playerMagazineEvent;

	public float attackDelay = 0.2f;
	[HideInInspector] public float attackDelayTimer;
	
	public float cooldown;
	[HideInInspector] public float cooldownTimer;
	public Action<Vector3> OnAttackEvent;
	[SerializeField] public Transform[] bulletFirePositions;

	[HideInInspector] public Vector3 AttackDirection;

	public bool IsAttack { get; private set; }

	[SerializeField] private PoolingType _bulletPoolingType;
	
	public void HandleAttackUpdate(bool value)
	{
		IsAttack = value;
	}
	
	public void Attack()
	{
		if (attackDelayTimer > 0 || curMagazine <= 0) return;
		attackDelayTimer = attackDelay;
		--curMagazine;
		OnAttackEvent?.Invoke(AttackDirection);
		playerMagazineEvent?.Invoke(curMagazine, maxMagazine);
		
		for (int i = 0; i < bulletFirePositions.Length; ++i)
		{
			Bullet bullet = PoolingManager.Instance.Pop(
				_bulletPoolingType, 
				bulletFirePositions[i].position, 
				Quaternion.LookRotation(AttackDirection)) as Bullet;
		}
		
		
		if (curMagazine <= 0)
		{
			cooldownTimer = cooldown;
		}
	}

	public void SetDelay()
	{
		if (attackDelay > 0)
		{
			attackDelayTimer -= Time.deltaTime;
			if (attackDelayTimer <= 0)
			{
				attackDelayTimer = 0;
			}
		}
	}
	
	public void SetCooldown()
	{
		if (cooldown > 0)
		{
			cooldownTimer -= Time.deltaTime;
			if (cooldownTimer <= 0)
			{
				cooldownTimer = cooldown;
				curMagazine = maxMagazine;
				playerMagazineEvent?.Invoke(curMagazine, maxMagazine);
			}
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
		
		//Magazine
		magazineInfoL.curMagazine = magazineInfoL.maxMagazine;
		magazineInfoR.curMagazine = magazineInfoR.maxMagazine;
		
		_inputReader.AttackLEvent += magazineInfoL.HandleAttackUpdate;
		_inputReader.AttackREvent += magazineInfoR.HandleAttackUpdate;
	}

	private void OnDestroy()
	{
		_inputReader.AttackLEvent -= magazineInfoL.HandleAttackUpdate;
		_inputReader.AttackREvent -= magazineInfoR.HandleAttackUpdate;
	}

	protected virtual void Update()
	{
		if (magazineInfoL.IsAttack)
			magazineInfoL.Attack();
		
		if (magazineInfoR.IsAttack)
			magazineInfoR.Attack();
		
		magazineInfoL.SetDelay();
		magazineInfoR.SetDelay();
		
		magazineInfoL.SetCooldown();
		magazineInfoR.SetCooldown();
	}
	
	protected void FixedUpdate()
	{
		magazineInfoL.AttackDirection = magazineInfoR.AttackDirection = transform.rotation * Vector3.forward;
	}
}
