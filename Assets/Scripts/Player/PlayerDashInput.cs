using UnityEngine;
using System.Collections;

public class PlayerDashInput : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashCooldown = 5f;
    public float dashDuration = 0.3f;
    public float baseDashDistance = 1f; // Base distance for multiplier 1 (for size 1.0)
    
    [Header("References")]
    public LevelSystem levelSystem; // Assign manually in Inspector
    
    private float lastDashTime = 0f;
    private bool isDashInputActive = false;
    private int dashMultiplier = 0;
    private Vector2 dashDirection = Vector2.zero;
    
    // Events for dash system
    public System.Action<int, Vector2> OnDashInput; // multiplier, direction
    
    void Update()
    {
        HandleDashInput();
    }
    
    private void HandleDashInput()
    {
        // Check for number keys (1-9)
        if (Input.GetKeyDown(KeyCode.Alpha1)) StartDashInput(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) StartDashInput(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) StartDashInput(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) StartDashInput(4);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) StartDashInput(5);
        // else if (Input.GetKeyDown(KeyCode.Alpha6)) StartDashInput(6);
        // else if (Input.GetKeyDown(KeyCode.Alpha7)) StartDashInput(7);
        // else if (Input.GetKeyDown(KeyCode.Alpha8)) StartDashInput(8);
        // else if (Input.GetKeyDown(KeyCode.Alpha9)) StartDashInput(9);
        
        // If we have an active dash input, check for direction keys
        if (isDashInputActive)
        {
            CheckDirectionInput();
        }
    }
    
    private void StartDashInput(int multiplier)
    {
        // Check if dash is unlocked
        Player player = GetComponent<Player>();
        if (player != null && !player.IsDashUnlocked())
        {
            return;
        }
        
        // Check cooldown
        if (Time.time - lastDashTime < dashCooldown)
        {
            return;
        }
        
        dashMultiplier = multiplier;
        isDashInputActive = true;
        dashDirection = Vector2.zero;
        
        // Start timeout for direction input
        StartCoroutine(DashInputTimeout());
    }
    
    private void CheckDirectionInput()
    {
        Vector2 direction = Vector2.zero;
        bool directionPressed = false;
        
        // Check for direction keys (vim-style)
        if (Input.GetKeyDown(KeyCode.J)) // Down
        {
            direction = Vector2.down;
            directionPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.K)) // Up
        {
            direction = Vector2.up;
            directionPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.L)) // Right
        {
            direction = Vector2.right;
            directionPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.H)) // Left
        {
            direction = Vector2.left;
            directionPressed = true;
        }
        
        if (directionPressed)
        {
            dashDirection = direction;
            ExecuteDash();
        }
    }
    
    private void ExecuteDash()
    {
        if (OnDashInput != null)
        {
            OnDashInput(dashMultiplier, dashDirection);
        }
        
        lastDashTime = Time.time;
        isDashInputActive = false;
    }
    
    /// <summary>
    /// Gets the effective dash distance based on player size
    /// </summary>
    public float GetEffectiveDashDistance()
    {
        if (levelSystem != null)
        {
            float playerSize = levelSystem.GetCurrentSize();
            return baseDashDistance * playerSize;
        }
        return baseDashDistance; // Fallback if no level system assigned
    }
    
    private IEnumerator DashInputTimeout()
    {
        yield return new WaitForSeconds(0.5f); // 0.5 second timeout for direction input
        
        if (isDashInputActive)
        {
            isDashInputActive = false;
        }
    }
    
    public bool CanDash()
    {
        return Time.time - lastDashTime >= dashCooldown;
    }
    
    public float GetCooldownRemaining()
    {
        return Mathf.Max(0, dashCooldown - (Time.time - lastDashTime));
    }
}
