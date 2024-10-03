using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerState<PlayerStateEnum>
{
	PlayerMovement playerMovement;
	PlayerDashEffectCaster _playerDashEffectCaster;

	private float _effectDelay = 0.025f;
	private float _curEffectDelay = 0.1f;

    public PlayerDashState(Player playerBase, PlayerStateMachine<PlayerStateEnum> stateMachine, string animBoolName) : base(playerBase, stateMachine, animBoolName)
    {
		playerMovement = playerBase.MovementCompo as PlayerMovement;
		_playerDashEffectCaster = playerBase.playerDashEffectCaster;
	}

	public override void Enter()
	{
		base.Enter();
		float duration = 0.65f;
		playerMovement.OnDash(playerMovement.transform.forward, duration, playerMovement.dashPower);
		_playerBase.StartCoroutine(CoroutineOnDashEnd(duration));
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

	private IEnumerator CoroutineOnDashEnd(float dashDuration)
	{
		yield return new WaitForSeconds(dashDuration);
		_stateMachine.ChangeState(PlayerStateEnum.Idle);
	}

	public override void Exit()
	{
		base.Exit();
	}
}
