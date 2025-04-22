using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Header("Wandering Settings")]
    [Tooltip("Enable this to allow the NPC to wander around.")]
    public bool canWalk = false;      // Toggle for wandering behavior
    public float walkSpeed = 2f;      // Movement speed
    public float wanderRadius = 5f;   // Radius (in world units) around the spawn point to wander
    public float waitTime = 2f;       // How long the NPC waits before choosing a new destination

    private Vector3 startPos;         // Record of the spawn point
    private Vector3 targetPos;        // Current destination
    private bool isMoving = false;    // Indicates if the NPC is currently moving towards a destination
    private float waitTimer = 0f;     // Timer for waiting between moves

    void Start()
    {
        // Record the starting position.
        startPos = transform.position;
        
        // If wandering is enabled, choose an initial destination.
        if (canWalk)
        {
            PickNewDestination();
        }
    }

    void Update()
    {
        // Only update movement if wandering is enabled.
        if (canWalk)
        {
            if (!isMoving)
            {
                // Count down while waiting at the destination.
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0f)
                {
                    PickNewDestination();
                }
            }
            else
            {
                // Move toward the target destination.
                transform.position = Vector3.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);
                
                // Gradually rotate to face the direction we're moving.
                Vector3 direction = targetPos - transform.position;
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
                }

                // When within close range to the destination, stop moving and start waiting.
                if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                {
                    isMoving = false;
                    waitTimer = waitTime;
                }
            }
        }
    }

    // Chooses a new random destination within a circle around the start position.
    void PickNewDestination()
    {
        Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
        targetPos = startPos + new Vector3(randomPoint.x, 0f, randomPoint.y);
        isMoving = true;
    }
    
    public void MoveToPoint(Vector3 point)
    {
        targetPos = point;
        isMoving = true;
        waitTimer = 0f;
    }
}

