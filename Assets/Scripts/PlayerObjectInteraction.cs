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
    private Quaternion grabbedObjectRotation;
    private bool grabbing = false;
    public AudioSource audioSource;
    public AudioClip grabSfx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) && !grabbing)
        {
            print("LEFT CLICK");
            RaycastHit hit1, hit2, hit3, hit4, hit5;
            Vector3 verticalRaycastOffset = new Vector3(0, 0.5f, 0);

            bool raycast1 = Physics.Raycast(transform.position, transform.forward, out hit1, grabDistance, layerMask);
            bool raycast2 = Physics.Raycast(transform.position + verticalRaycastOffset, transform.forward, out hit2, grabDistance, layerMask);
            bool raycast3 = Physics.Raycast(transform.position - verticalRaycastOffset, transform.forward, out hit3, grabDistance, layerMask);
            bool raycast4 = Physics.Raycast(transform.position - verticalRaycastOffset/2, transform.forward, out hit4, grabDistance, layerMask);
            bool raycast5 = Physics.Raycast(transform.position + verticalRaycastOffset / 2, transform.forward, out hit5, grabDistance, layerMask);
            UnityEngine.Debug.DrawRay(transform.position, transform.forward * grabDistance, Color.red);

            RaycastHit hit; // Final selected hit to use

            if (raycast1)
                hit = hit1;
            else if (raycast2)
                hit = hit2;
            else if (raycast3)
                hit = hit3;
            else if (raycast4)
                hit = hit4;
            else if (raycast5)
                hit = hit5;
            else
                return; // Nothing hit

            if (hit.transform.CompareTag("InteractableObject"))
            {
                audioSource.PlayOneShot(grabSfx, 0.5f);
                print("GRABBE");
                grabbing = true;
                grabbedObject = hit.transform.gameObject;
                distToGrabbedObject = transform.InverseTransformPoint(grabbedObject.transform.position);
                grabbedObjectRigidbody = grabbedObject.GetComponent<Rigidbody>();
                grabbedObjectRigidbody.useGravity = false;
                grabbedObjectRotation = Quaternion.Inverse(transform.rotation) * grabbedObject.transform.rotation;
            }

        }
        if (grabbing && (Input.GetMouseButton(0) || Input.GetKey(KeyCode.E)))
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
        else if (grabbing && (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.E)))
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
            Quaternion desiredRotation = transform.rotation * grabbedObjectRotation;

            grabbedObjectRigidbody.MovePosition(desiredPosition);
            grabbedObjectRigidbody.MoveRotation(desiredRotation);
        }
    }
}
