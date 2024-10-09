using System;
using System.Collections.Generic;
using UnityEngine;

public class MassProductionDrone : MonoBehaviour
{
	private Player _player;
	public Transform currentTarget;

	public bool isAttacking;
	private uint _level;

	private MPDroneStateMachine _mpStateMachine;

	public MassProductionDroneAttack attackCompo;
	public MassProductionDroneMovement movementCompo;

	public event Action OnEnabledEvent;

	public void InitLevel(uint level)
	{
		_level = level;
	}

	private void OnEnable()
	{
		OnEnabledEvent?.Invoke();
	}

	private void Awake()
	{
		attackCompo = GetComponent<MassProductionDroneAttack>();
		movementCompo = GetComponent<MassProductionDroneMovement>();
	}

	private void Start()
	{
		_player = GameManager.Instance.Player;
		_mpStateMachine = new MPDroneStateMachine(this);
	}

	private void Update()
	{
		_mpStateMachine?.StateUpdate();
	}
}
