using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.movement != null)
        {
            // Update movement based on current input
            player.movement.UpdateMovement();
        }

        // Check if we should transition to idle state
        if (player.Speed == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
