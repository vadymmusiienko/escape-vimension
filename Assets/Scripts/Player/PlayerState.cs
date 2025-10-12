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
        // Animation handling would go here if animator exists
        // player.anim?.SetBool(animBoolName, true);
    }

    public virtual void Exit() 
    {
        // Animation handling would go here if animator exists
        // player.anim?.SetBool(animBoolName, false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        
        // The PlayerMovement component handles input and gravity automatically
        // States just need to handle state-specific logic
    }
}
