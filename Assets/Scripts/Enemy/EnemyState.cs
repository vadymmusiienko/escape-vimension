using UnityEngine;

public abstract class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    private string animBool;

    public EnemyState(Enemy enemy,  EnemyStateMachine stateMachine, string animBool)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBool = animBool;
    }

    public virtual void Enter()
    {
        if (enemy.anim != null)
        {
            enemy.anim.SetBool(animBool, true);
        }
    }

    public virtual void Exit()
    {
        if (enemy.anim != null)
        {
            enemy.anim.SetBool(animBool, false);
        }
    }

    public virtual void Update()
    {
    }
}
