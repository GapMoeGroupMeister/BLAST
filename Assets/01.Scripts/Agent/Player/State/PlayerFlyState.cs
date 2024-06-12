public class PlayerFlyState : PlayerState<PlayerStateEnum>
{
    public PlayerFlyState(Player playerBase, PlayerStateMachine<PlayerStateEnum> stateMachine, string animBoolName) : base(playerBase, stateMachine, animBoolName)
    {
    }
}
