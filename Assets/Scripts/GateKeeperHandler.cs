using System;
using UnityEngine;

public class GateKeeperHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public NPCMovement movementScript;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveGate()
    {
        movementScript.MoveToPoint(transform.position + new Vector3(1f, 0f, 15f));
    }
}
