using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    // Input values
    public float InputX { get; private set; }
    public float InputY { get; private set; }
    public float Speed { get; private set; }
    
    // Input flags
    public bool AttackPressed { get; private set; }
    public bool PickupPressed { get; private set; }
    public bool CopyPressed { get; private set; }
    public bool PastePressed { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        HandleMovementInput();
        HandleActionInput();
    }
    
    private void HandleMovementInput()
    {
        InputX = 0;
        InputY = 0;
        
        if (Input.GetKey(KeyCode.H)) InputX = -1;
        if (Input.GetKey(KeyCode.L)) InputX = 1;
        if (Input.GetKey(KeyCode.J)) InputY = -1;
        if (Input.GetKey(KeyCode.K)) InputY = 1;
        
        Speed = Mathf.Abs(InputX) + Mathf.Abs(InputY);
    }
    
    private void HandleActionInput()
    {
        AttackPressed = Input.GetKeyDown(KeyCode.D);
        PickupPressed = Input.GetKeyDown(KeyCode.X);
        CopyPressed = Input.GetKeyDown(KeyCode.Y);
        PastePressed = Input.GetKeyDown(KeyCode.P);
    }
    
    public void ConsumeInputs()
    {
        AttackPressed = false;
        PickupPressed = false;
        CopyPressed = false;
        PastePressed = false;
    }
}
