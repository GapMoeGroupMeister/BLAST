using UnityEngine;

public class PlayerWalkState : PlayerState<PlayerStateEnum>
{
    private Vector3 _moveDirection;
    
    public PlayerWalkState(Player playerBase, PlayerStateMachine<PlayerStateEnum> stateMachine, string animBoolName) : base(playerBase, stateMachine, animBoolName)
    {
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
}
