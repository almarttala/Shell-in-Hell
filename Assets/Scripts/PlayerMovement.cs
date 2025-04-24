using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    // Movement variables
    public float speed = 5f;
    public float dashMultiplier = 2f;
    public float rotationSpeed = 180f;
    
    // Dash energy parameters
    public float dashEnergyMax = 2f;      // Maximum dash energy (in seconds)
    public float dashRechargeTime = 8f;   // Time (in seconds) to fully recharge dash energy
    private float dashEnergy;             // Current dash energy
    public AudioSource audioSource;
    // Dash ability bar
    public Image dashAbilityBar;
    public bool walkSoundPlaying = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        // Initialize dash energy to full at the start.
        dashEnergy = dashEnergyMax;
    }

    void Update()
    {
        HandleMovement();
        UpdateDashUI();
    }

void HandleMovement()
    {
        // Get input for rotation (A/D keys) and forward/backward movement (W/S keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate the player around its local y-axis.
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        // Determine movement speed based on dash input and energy.
        float currentSpeed = speed;

        // Check if the dash key (Space) is held.
        if (Input.GetKey(KeyCode.Space))
        {
            // Only apply dash if there's dash energy available.
            if (dashEnergy > 0f)
            {
                currentSpeed = speed * dashMultiplier;
                dashEnergy -= Time.deltaTime; // Deplete dash energy while dashing
                if (dashEnergy < 0f)
                    dashEnergy = 0f;
            }
            else
            {
                // Dash energy is empty so continue at normal speed.
                currentSpeed = speed;
            }
        }
        else
        {
            // Recharge dash energy when dash is not active.
            float rechargeRate = dashEnergyMax / dashRechargeTime;
            dashEnergy += rechargeRate * Time.deltaTime;
            if (dashEnergy > dashEnergyMax)
                dashEnergy = dashEnergyMax;
        }

        // Move the player forward or backward relative to its own orientation.
        transform.Translate(Vector3.forward * verticalInput * currentSpeed * Time.deltaTime, Space.Self);
        if (verticalInput != 0 && !walkSoundPlaying)
        {
            print("START PLAYING SOUND");
            audioSource.Play();
            walkSoundPlaying = true;
        }
        else if (walkSoundPlaying && verticalInput == 0)
        {
            audioSource.Stop();
            walkSoundPlaying = false;
        }    
    }

    void UpdateDashUI()
    {
        // Update the UI dash bar's fill amount to reflect current dash energy.
        if (dashAbilityBar != null)
        {
            dashAbilityBar.fillAmount = dashEnergy / dashEnergyMax;
        }
    }
}
