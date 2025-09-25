using UnityEngine;

public class Player : Entity
{
    public float moveSpeed;
    public CharacterController controller {  get; private set; }
    public Camera cam { get; private set; }
    public float InputX {  get; set; }
    public float InputY { get; set; }
    public float Speed { get; set; }

    public Vector3 desiredMoveDirection;
    public float desiredRotationSpeed = 0.1f;
    public float allowPlayerRotation = 0.1f;

    public float verticalVel;
    public bool isGrounded;

    #region States
    public PlayerStateMachine stateMachine;
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    #endregion


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        controller = GetComponent<CharacterController>();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
    }
    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currState.Update();
    }
}
