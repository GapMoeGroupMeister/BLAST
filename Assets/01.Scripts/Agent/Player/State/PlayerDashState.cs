using Crogen.PowerfulInput;
using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerState<PlayerStateEnum>
{
	PlayerMovement playerMovement;
	AgentDashEffectCaster _playerDashEffectCaster;

	private float _effectDelay = 0.025f;
	private float _curEffectDelay = 0.1f;

	private InputReader _inputReader;

    public PlayerDashState(Player playerBase, PlayerStateMachine<PlayerStateEnum> stateMachine, string animBoolName) : base(playerBase, stateMachine, animBoolName)
    {
		_inputReader = GameManager.Instance.InputReader;
		playerMovement = playerBase.MovementCompo as PlayerMovement;
		_playerDashEffectCaster = playerBase.playerDashEffectCaster;
	}

	public override void Enter()
	{
		base.Enter();
		float duration = 0.65f;
		
		playerMovement.OnDash(
			playerMovement.transform.forward, 
			duration, 
			playerMovement.dashPower,
			()=> OnDashEndHandle());
	}

	private void OnDashEndHandle()
	{
		if (Mathf.Approximately(_inputReader.Movement.sqrMagnitude, 0f))
			_stateMachine.ChangeState(PlayerStateEnum.Idle);
		else
			_stateMachine.ChangeState(PlayerStateEnum.Walk);
	}

	public override void FixedUpdateState()
	{
		base.FixedUpdateState();
		_curEffectDelay += Time.deltaTime;
		if (_curEffectDelay > _effectDelay)
		{
			_curEffectDelay = 0;
			_playerDashEffectCaster.CreateDashEffect();
		}
	}
}