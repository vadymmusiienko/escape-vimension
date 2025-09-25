using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;

    protected float stateTimer;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit() 
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        player.InputX = 0;
        player.InputY = 0;
        if (Input.GetKey(KeyCode.H))
        {
            player.InputX = -1;
        }
        if (Input.GetKey(KeyCode.L))
        {
            player.InputX = 1;
        }
        if (Input.GetKey(KeyCode.J))
        {
            player.InputY = -1;
        }
        if (Input.GetKey(KeyCode.K))
        {
            player.InputY = 1;
        }
        player.Speed = Mathf.Abs(player.InputX) + Mathf.Abs(player.InputY);
        if (player.isGrounded)
        {
            player.verticalVel = -2f;
        }
        else
        {
            player.verticalVel -= 9.8f * Time.deltaTime;
        }
        Vector3 gravityMove = new Vector3(0, player.verticalVel * Time.deltaTime, 0);
        player.controller.Move(gravityMove);
    }
}
