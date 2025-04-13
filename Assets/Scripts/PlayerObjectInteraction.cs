using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class PlayerObjectInteraction : MonoBehaviour
{
    //public Transform transform;
    private LayerMask layerMask;
    public GameObject grabbedObject;
    public Rigidbody grabbedObjectRigidbody;
    public float grabDistance;
    private Vector3 distToGrabbedObject;
    private bool grabbing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !grabbing)
        {
            print("LEFT CLICK");
            RaycastHit hit;
            UnityEngine.Debug.DrawRay(transform.position, transform.forward * grabDistance, Color.red);
            if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, layerMask))
            {

                if (hit.transform.gameObject.tag == "InteractableObject")
                {
                    print("GRABBE");
                    grabbing = true;
                    grabbedObject = hit.transform.gameObject;
                    distToGrabbedObject = transform.InverseTransformPoint(grabbedObject.transform.position);
                    grabbedObjectRigidbody = grabbedObject.GetComponent<Rigidbody>();
                    grabbedObjectRigidbody.useGravity = false;
                }

            }
        }
        if (grabbing && Input.GetMouseButton(0))
        {
 /*           Vector3 desiredPosition = transform.TransformPoint(distToGrabbedObject);
            float currentDistance = Vector3.Distance(grabbedObject.transform.position, transform.position);

            float minDistance = grabDistance - 0.5f;
            float maxDistance = grabDistance + 0.5f;

            if (currentDistance < minDistance || currentDistance > maxDistance)
            {
                // Snap it back to desired position
                grabbedObject.transform.position = desiredPosition;
            }*/
        }
        else if (grabbing && Input.GetMouseButtonUp(0))
        {
            grabbing = false;
            grabbedObject = null;
            if (grabbedObjectRigidbody != null)
            {
                grabbedObjectRigidbody.useGravity = true;
                grabbedObjectRigidbody.constraints = RigidbodyConstraints.None;
            }
            grabbedObjectRigidbody = null;
        }
    }

    void FixedUpdate()
    {
        if (grabbing && grabbedObjectRigidbody != null)
        {
            Vector3 desiredPosition = transform.TransformPoint(distToGrabbedObject);
            Quaternion desiredRotation = transform.rotation;

            grabbedObjectRigidbody.MovePosition(desiredPosition);
            grabbedObjectRigidbody.MoveRotation(desiredRotation);
        }
    }
}
