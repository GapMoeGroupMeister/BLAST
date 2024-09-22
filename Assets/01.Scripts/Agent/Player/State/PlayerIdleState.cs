using System;

public class PlayerIdleState : PlayerState<PlayerStateEnum>
{
    PlayerMovement _playerMovement;

    public PlayerIdleState(Player playerBase, PlayerStateMachine<PlayerStateEnum> stateMachine, string animBoolName) : base(playerBase, stateMachine, animBoolName)
    {
        _playerMovement = _playerBase.MovementCompo as PlayerMovement;
    }

    public override void Enter()
    {
        base.Enter();
        _playerBase.MovementCompo.StopImmediately();
        _gameManager.InputReader.MoveStartEvent += HandleMoveStart;
        _gameManager.InputReader.DashEvent += HandleOnDash;
    }

	public override void Exit()
    {
        _gameManager.InputReader.MoveStartEvent -= HandleMoveStart;
        _gameManager.InputReader.DashEvent -= HandleOnDash;
        base.Exit();
    }

    private void HandleMoveStart()
    {
        _stateMachine.ChangeState(PlayerStateEnum.Walk);
    }

    private void HandleOnDash()
    {
        if(_playerMovement.canDash)
            _stateMachine.ChangeState(PlayerStateEnum.Dash);
    }
}