using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float dashMultiplier = 2f;
    public float rotationSpeed = 180f;
    
    public float dashEnergyMax = 2f;
    public float dashRechargeTime = 8f;
    private float dashEnergy;
    
    public AudioSource audioSource;
    public Image dashAbilityBar;
    public bool walkSoundPlaying = false;

    void Start()
    {
        dashEnergy = dashEnergyMax;
    }

    void Update()
    {
        HandleMovement();
        UpdateDashUI();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate player around Y-axis (local rotation)
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        // Determine movement speed based on dash input and energy
        float currentSpeed = speed;

        if (Input.GetKey(KeyCode.Space) && dashEnergy > 0f)
        {
            currentSpeed = speed * dashMultiplier;
            dashEnergy -= Time.deltaTime;
            if (dashEnergy < 0f) dashEnergy = 0f;
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            float rechargeRate = dashEnergyMax / dashRechargeTime;
            dashEnergy += rechargeRate * Time.deltaTime;
            if (dashEnergy > dashEnergyMax) dashEnergy = dashEnergyMax;
        }

        // Move forward/backward in world space relative to player's current rotation
        Vector3 movementDirection = transform.forward * verticalInput * currentSpeed * Time.deltaTime;
        transform.Translate(movementDirection, Space.World);

        // Handle walk sound
        if (verticalInput != 0 && !walkSoundPlaying)
        {
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
        if (dashAbilityBar != null)
        {
            dashAbilityBar.fillAmount = dashEnergy / dashEnergyMax;
        }
    }
}