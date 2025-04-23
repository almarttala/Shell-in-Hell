using System.Diagnostics;
using UnityEngine;

public class BoulderMovement : MonoBehaviour
{
    public Transform pointOnRailA; // Start of the rail
    public Transform pointOnRailB; // End of the rail
    public Transform targetPoint;  // The target the object should move toward (along the rail)

    public float moveForce = 10f;
    public float maxWiggle = 1f;

    private Rigidbody rb;
    public bool onRails = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (onRails)
        {


            // Define the rail vector
            Vector3 railDirection = (pointOnRailB.position - pointOnRailA.position).normalized;

            // Project current position onto the rail
            Vector3 closestPointOnRail = ProjectPointOntoLine(pointOnRailA.position, railDirection, transform.position);

            // Wiggle logic: allow slight deviation from rail
            Vector3 offset = transform.position - closestPointOnRail;
            if (offset.magnitude > maxWiggle)
            {
                Vector3 correction = -offset.normalized * (offset.magnitude - maxWiggle);
                rb.AddForce(correction * moveForce);
            }

            // Move towards the target (only along rail)
            Vector3 toTarget = targetPoint.position - transform.position;
            float alongRail = Vector3.Dot(toTarget, railDirection);
            Vector3 movement = railDirection * alongRail;
            rb.AddForce(movement.normalized * moveForce);
            if (transform.position == targetPoint.position)
            {
                onRails = false;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        print("BEEP");
        if (other.gameObject.name == targetPoint.name)
        {
            onRails = false;
        }
    }

    // Utility: Projects a point onto a line defined by origin and direction
    Vector3 ProjectPointOntoLine(Vector3 origin, Vector3 direction, Vector3 point)
    {
        return origin + Vector3.Project(point - origin, direction);
    }
}
