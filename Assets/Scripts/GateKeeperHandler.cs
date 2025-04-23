using System;
using System.Diagnostics;
using UnityEngine;

public class GateKeeperHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public NPCMovement movementScript;
    public int annoyCount = 0;
    public int annoyCountMax = 5;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        GameObject boulder = GameObject.Find("Boulder");
        if (boulder != null)
        {
            float distance = Vector3.Distance(transform.position, boulder.transform.position);

            if (distance <= 15f)
            {
                movementScript.walkSpeed = 8f;
            }
            else
            {

            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Boulder not found in the scene.");
        }
    }

    public void LeaveGate()
    {
        movementScript.MoveToPoint(transform.position + new Vector3(1f, 0f, -15f));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Pitchfork")
        {
            annoyCount++;
            UnityEngine.Debug.Log("Pitchfork collided! annoyCounter: " + annoyCount);
        }
    }
}
