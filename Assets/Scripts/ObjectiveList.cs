using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ObjectiveList : MonoBehaviour
{
    public List<Objective> objectives = new List<Objective>();

    void Start()
    {
        AddObjective("Meet The Devil.");
        AddObjective("Get past the gate.");
        // Example: add an objective with a trigger function
        AddObjective("Bribe the guard.", () => CheckBribeObjective(), () => UnityEngine.Debug.Log("PLACEHOLDER FOR BRIBE OBJ COMPLETED"));
    }

    void Update()
    {

    }
    bool CheckBribeObjective()
    {
        GameObject gold = GameObject.Find("Gold");
        GameObject triggerZone = GameObject.Find("GoldObjTriggerZone");

        if (gold != null && triggerZone != null)
        {
            Collider triggerCollider = triggerZone.GetComponent<Collider>();

            if (triggerCollider != null)
            {
                // Check if gold's position is within the bounds of the trigger zone
                if (triggerCollider.bounds.Contains(gold.transform.position))
                {
                    UnityEngine.Debug.Log("Gold is inside the trigger zone!");
                    return true;
                    // Do something here like completing the objective
                }
                else
                {
                    //UnityEngine.Debug.Log("Gold exists but is NOT inside the trigger zone.");
                    return false;
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning("WARNING: Trigger zone exists but does not have a Collider component.");
                return false;
            }
        }
        else
        {
            UnityEngine.Debug.Log("WARNING: Gold or trigger zone not found in the scene.");
            return false;
        }
    }

    void FixedUpdate()
    {
        foreach (var obj in objectives)
        {
            if (!obj.isComplete)
            {
                obj.CheckIfCompleted();
                if (obj.isComplete)
                {
                    UnityEngine.Debug.Log($"Objective Complete: {obj.name}");
                }
            }
        }
    }

    public void AddObjective(string name, System.Func<bool> trigger = null, Action onComplete = null)
    {
        Objective newObj = new Objective(name, trigger, onComplete);
        objectives.Add(newObj);
    }
}
