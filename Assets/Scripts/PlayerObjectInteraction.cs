using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
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
                    distToGrabbedObject = grabbedObject.transform.position - transform.position;
                }
            }
        }
        if (grabbing && Input.GetMouseButton(0))
        {
            Vector3 desiredPosition = transform.position + distToGrabbedObject;
            float currentDistance = Vector3.Distance(grabbedObject.transform.position, transform.position);

            float minDistance = grabDistance - 0.5f;
            float maxDistance = grabDistance + 0.5f;

            if (currentDistance < minDistance || currentDistance > maxDistance)
            {
                // Snap it back to desired position
                grabbedObject.transform.position = desiredPosition;
            }
        }
        else if (grabbing && Input.GetMouseButtonUp(0))
        {
            grabbing = false;
            grabbedObject = null;
        }
    }

    void FixedUpdate()
    {
        
    }
}
