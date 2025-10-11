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

        if (player.Speed > 0)
        {
            var camera = player.cam;
            var forward = camera.transform.forward;
            var right = camera.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            player.desiredMoveDirection = forward * player.InputY + right * player.InputX;

            Vector3 moveInput = new Vector3(player.InputX, 0, player.InputY);
            if (moveInput.magnitude > 0)
            {
                player.transform.rotation = Quaternion.Slerp(
                    player.transform.rotation,
                    Quaternion.LookRotation(player.desiredMoveDirection),
                    player.desiredRotationSpeed
                );
            }

            Vector3 horizontalMove = player.desiredMoveDirection * Time.deltaTime * player.moveSpeed;
            player.controller.Move(horizontalMove);
        }

        if (player.Speed == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
