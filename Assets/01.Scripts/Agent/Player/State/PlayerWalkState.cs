using UnityEngine;

public class PlayerWalkState : PlayerState<PlayerStateEnum>
{
    PlayerMovement _playerMovement;
    private Vector3 _moveDirection;
    
    public PlayerWalkState(Player playerBase, PlayerStateMachine<PlayerStateEnum> stateMachine, string animBoolName) : base(playerBase, stateMachine, animBoolName)
    {
        _playerMovement = _playerBase.MovementCompo as PlayerMovement;
    }

    public override void Enter()
	{
		base.Enter();
        _gameManager.InputReader.DashEvent += HandleOnDash;
    }

    public override void Exit()
	{
        _gameManager.InputReader.DashEvent -= HandleOnDash;
        base.Exit();
	}

	public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        _playerBase.MovementCompo.SetMovement(_gameManager.InputReader.Movement);
        if(_gameManager.InputReader.Movement.sqrMagnitude < 0.1f)
		{
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    private void HandleOnDash()
    {
        if (_playerMovement.canDash)
            _stateMachine.ChangeState(PlayerStateEnum.Dash);
    }
}
