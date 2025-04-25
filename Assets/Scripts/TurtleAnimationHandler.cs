using UnityEngine;

public class TurtleAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerObjectInteraction objectInteraction;

    // Animator parameter names
    private const string PARAM_SPEED = "Speed";
    private const string PARAM_GRAB = "Grab";
    private const string PARAM_RELEASE = "Release";
    private const string PARAM_HIDE = "Hide";

    // Debug settings
    [SerializeField] private bool debugMode = true; // Changed to true by default for troubleshooting
    [SerializeField] private float speedThreshold = 0.1f; // Add a configurable speed threshold

    void Start()
    {
        // Get references to required components
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject!");
            enabled = false;
            return;
        }

        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogWarning("No PlayerMovement component found on this GameObject!");
        }

        objectInteraction = GetComponent<PlayerObjectInteraction>();
        if (objectInteraction == null)
        {
            Debug.LogWarning("No PlayerObjectInteraction component found on this GameObject!");
        }

        // Verify that Animator Controller has required parameters
        VerifyAnimatorParameters();
    }

    void VerifyAnimatorParameters()
    {
        bool hasSpeed = false;
        bool hasGrab = false;
        bool hasRelease = false;
        bool hasHide = false;

        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == PARAM_SPEED && param.type == AnimatorControllerParameterType.Float)
                hasSpeed = true;
            else if (param.name == PARAM_GRAB && param.type == AnimatorControllerParameterType.Trigger)
                hasGrab = true;
            else if (param.name == PARAM_RELEASE && param.type == AnimatorControllerParameterType.Trigger)
                hasRelease = true;
            else if (param.name == PARAM_HIDE && param.type == AnimatorControllerParameterType.Trigger)
                hasHide = true;
        }

        if (!hasSpeed || !hasGrab || !hasRelease || !hasHide)
        {
            Debug.LogError("Animator Controller is missing required parameters! Make sure you have:" +
                "\n- Speed (Float)" +
                "\n- Grab (Trigger)" +
                "\n- Release (Trigger)" +
                "\n- Hide (Trigger)");
        }
    }

    void Update()
    {
        UpdateMovementAnimation();
        UpdateGrabAnimation();
        UpdateHideAnimation();
    }

    void UpdateMovementAnimation()
    {
        if (playerMovement == null) return;

        // Calculate movement magnitude from input
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float movementMagnitude = Mathf.Abs(verticalInput);

        // Set the Speed parameter on the animator
        animator.SetFloat(PARAM_SPEED, movementMagnitude);

        if (debugMode)
        {
            // Enhanced debugging information
            Debug.Log($"Vertical Input: {verticalInput}");
            Debug.Log($"Movement Magnitude: {movementMagnitude}");
            Debug.Log($"Speed Threshold: {speedThreshold}");
            Debug.Log($"Is Moving: {movementMagnitude > speedThreshold}");

            // Get current animator state information
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log($"Current State: {stateInfo.fullPathHash}");
            Debug.Log($"Normalized Time: {stateInfo.normalizedTime}");
        }
    }

    void UpdateGrabAnimation()
    {
        if (objectInteraction == null) return;

        // Detect grab action
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) && !objectInteraction.grabbing)
        {
            // We're starting to grab - trigger grab animation
            // Note: The actual grabbing logic is in PlayerObjectInteraction
            animator.SetTrigger(PARAM_GRAB);
            
            if (debugMode)
            {
                Debug.Log("Grab trigger set");
            }
        }

        // Detect release action
        if (objectInteraction.grabbing && (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.E)))
        {
            // We're releasing an object - trigger release animation
            animator.SetTrigger(PARAM_RELEASE);
            
            if (debugMode)
            {
                Debug.Log("Release trigger set");
            }
        }
    }

    void UpdateHideAnimation()
    {
        // Detect hide action (pressing H key)
        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger(PARAM_HIDE);
            
            if (debugMode)
            {
                Debug.Log("Hide trigger set");
            }
        }
    }

    // Utility method to get movement magnitude (can be called from other scripts if needed)
    public float GetMovementMagnitude()
    {
        if (playerMovement == null) return 0f;
        
        float verticalInput = Input.GetAxis("Vertical");
        return Mathf.Abs(verticalInput);
    }
}