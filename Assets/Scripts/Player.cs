using UnityEngine;

public class Player : Entity
{
    public float moveSpeed;
    public CharacterController controller {  get; private set; }
    public Camera cam { get; private set; }
    public CameraFollow cameraFollow { get; private set; }
    public float InputX {  get; set; }
    public float InputY { get; set; }
    public float Speed { get; set; }

    public Vector3 desiredMoveDirection;
    public float desiredRotationSpeed = 0.1f;
    public float allowPlayerRotation = -999f;

    public float verticalVel;
    public bool isGrounded;
    
    // Attack system
    private float lastAttackTime = 0f;
    public float attackCooldown = 1.5f;

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
        
        // Setup camera follow
        if (cam != null)
        {
            cameraFollow = cam.GetComponent<CameraFollow>();
            if (cameraFollow == null)
            {
                cameraFollow = cam.gameObject.AddComponent<CameraFollow>();
            }
            cameraFollow.SetTarget(transform);
        }
        
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        
        // Handle attack input independently of state
        if (Input.GetKeyDown(KeyCode.D) && CanAttack())
        {
            PerformAttack();
        }
        
        stateMachine.currState.Update();
    }
    
    private bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackCooldown;
    }
    
    private void PerformAttack()
    {
        // Trigger the attack animation (doesn't change state)
        anim.SetTrigger("Attack");
        lastAttackTime = Time.time;
        
        // Add attack logic here
        Debug.Log("Player attacking!");
        
        // TODO: Add damage detection, effects, etc.
    }
    
    // Camera control methods
    public void SetCameraOffset(Vector3 offset)
    {
        if (cameraFollow != null)
        {
            cameraFollow.SetOffset(offset);
        }
    }
    
    public void SetCameraFollowSpeed(float speed)
    {
        if (cameraFollow != null)
        {
            cameraFollow.SetFollowSpeed(speed);
        }
    }
    
    public void SnapCameraToPlayer()
    {
        if (cameraFollow != null)
        {
            cameraFollow.SnapToTarget();
        }
    }
}
