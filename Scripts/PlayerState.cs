using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;
    protected float xInput;
    protected float zInput;

    protected float stateTimer;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        //player.anim.SetBool(animBoolName, true);
        Debug.Log("Entered");
    }

    public virtual void Exit() 
    {
        //player.anim.SetBool(animBoolName, false);
        Debug.Log("Exited");
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = 0;
        zInput = 0;
        if (Input.GetKey(KeyCode.H))
        {
            xInput = -1;
        }
        if (Input.GetKey(KeyCode.L))
        {
            xInput = 1;
        }
        if (Input.GetKey(KeyCode.J))
        {
            zInput = -1;
        }
        if (Input.GetKey(KeyCode.K))
        {
            zInput = 1;
        }
    }
}
