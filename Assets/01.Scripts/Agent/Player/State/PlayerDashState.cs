using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerState<PlayerStateEnum>
{
	PlayerMovement playerMovement;

    public PlayerDashState(Player playerBase, PlayerStateMachine<PlayerStateEnum> stateMachine, string animBoolName) : base(playerBase, stateMachine, animBoolName)
    {
		playerMovement = playerBase.MovementCompo as PlayerMovement;
	}

	public override void Enter()
	{
		base.Enter();
		float duration = 0.65f;
		playerMovement.OnDash(playerMovement.transform.forward, duration, playerMovement.dashPower);
		_playerBase.StartCoroutine(CoroutineOnDashEnd(duration));
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
