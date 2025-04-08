using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    // Movement variables
    public float speed = 5f;
    public float dashMultiplier = 2f;
    
    // Dash timings (in seconds)
    public float dashDuration = 2f;
    public float dashCooldown = 8f;

    // Internal timers and state flags
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private bool isDashing = false;

    // Dash ability bar
    public Image dashAbilityBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // No initialization required atp
    }

        void Update()
    {
        HandleDashInputAndTimers();
        HandleMovement();
        UpdateDashUI();
    }

    void HandleDashInputAndTimers()
    {
        // Check if dash can be started (not dashing and not in cooldown)
        if (!isDashing && dashCooldownTimer <= 0f && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
            dashTimer = dashDuration;
        }

        // If currently dashing, count down the dash timer.
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
                dashCooldownTimer = dashCooldown;
            }
        }

        // If on cooldown, count down the cooldown timer.
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    void HandleMovement()
    {
        // Get input from WASD (or arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float verticalInput = Input.GetAxis("Vertical");       // W/S or Up/Down

        // Create a movement vector based on input.
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();

        // Choose the speed based on whether the player is dashing.
        float currentSpeed = isDashing ? speed * dashMultiplier : speed;

        // Move the player object. Multiplying by Time.deltaTime makes movement frame rate independent.
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
    }

    void UpdateDashUI()
    {
        if (dashAbilityBar == null)
            return;
        
        // Update the UI fill amount based on the dash state.
        if (isDashing)
        {
            // Bar shrinks during dash: from 1 to 0 over dashDuration.
            dashAbilityBar.fillAmount = dashTimer / dashDuration;
        }
        else if (dashCooldownTimer > 0f)
        {
            // During cooldown, the bar recharges: fill goes from 0 to 1 over dashCooldown.
            dashAbilityBar.fillAmount = 1f - (dashCooldownTimer / dashCooldown);
        }
        else
        {
            // Dash ready, bar is full.
            dashAbilityBar.fillAmount = 1f;
        }
    }
}
