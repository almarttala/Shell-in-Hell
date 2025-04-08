using UnityEngine;
using System.Collections;

public class TempMovementScript : MonoBehaviour
{

    public Transform PlayerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            PlayerTransform.position += PlayerTransform.forward * 0.05f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerTransform.position += -PlayerTransform.forward * 0.05f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerTransform.Rotate(Vector3.up, -0.5f); // Rotate left
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerTransform.Rotate(Vector3.up, 0.5f); // Rotate right
        }
    }

    void FixedUpdate()
    {

    }
}
