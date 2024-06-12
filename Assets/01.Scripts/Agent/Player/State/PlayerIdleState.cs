public class PlayerIdleState : PlayerState<PlayerStateEnum>
{
    public PlayerIdleState(Player playerBase, PlayerStateMachine<PlayerStateEnum> stateMachine, string animBoolName) : base(playerBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _playerBase.MovementCompo.StopImmediately();
        _gameManager.InputReader.MoveStartEvent += HandleMoveStart;
    }

    public override void Exit()
    {
        _gameManager.InputReader.MoveStartEvent -= HandleMoveStart;
        base.Exit();
    }

    private void HandleMoveStart()
    {
        _stateMachine.ChangeState(PlayerStateEnum.Walk);
    }
}