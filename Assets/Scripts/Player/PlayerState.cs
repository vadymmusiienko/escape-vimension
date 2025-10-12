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
        // Set animation bool to true when entering state
        if (player.anim != null)
        {
            player.anim.SetBool(animBoolName, true);
        }
    }

    public virtual void Exit() 
    {
        // Set animation bool to false when exiting state
        if (player.anim != null)
        {
            player.anim.SetBool(animBoolName, false);
        }
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        
        // Handle input and pass to movement component
        if (player.movement != null)
        {
            // Get input
            float inputX = 0;
            float inputY = 0;
            
            if (Input.GetKey(KeyCode.H)) inputX = -1;
            if (Input.GetKey(KeyCode.L)) inputX = 1;
            if (Input.GetKey(KeyCode.J)) inputY = -1;
            if (Input.GetKey(KeyCode.K)) inputY = 1;
            
            // Set input on movement component
            player.movement.InputX = inputX;
            player.movement.InputY = inputY;
            player.movement.Speed = Mathf.Abs(inputX) + Mathf.Abs(inputY);
        }
    }
}
