using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class ObjectiveList : MonoBehaviour
{
    public List<Objective> objectives = new List<Objective>();
    public GameObject pauseMenu;
    public List<GameObject> objectiveObjectList = new List<GameObject>();
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
                    CompleteObjective(obj.name);
                    UnityEngine.Debug.Log($"Objective Complete: {obj.name}");
                }
            }
        }
    }
    void CompleteObjective(string name)
    { 
        for (int index = 0; index < objectiveObjectList.Count; index++)
        {
            if (objectiveObjectList[index].name == "ObjectiveText" + name)
            {
                GameObject tmpObj = objectiveObjectList[index];
                TextMeshProUGUI tmp = tmpObj.GetComponent<TextMeshProUGUI>();
                tmp.text = "<s>" + name + "<s>";
            }
        }

    }
    public void AddObjective(string name, System.Func<bool> trigger = null, Action onComplete = null)
    {
        Objective newObj = new Objective(name, trigger, onComplete);
        objectives.Add(newObj);
        // Create a new GameObject
        int textYPos = 133;
        int textHeight = 50;
        int objectiveIndex = objectives.IndexOf(newObj);
        textYPos = textYPos - (textHeight * objectiveIndex);
        GameObject tmpObj = new GameObject("ObjectiveText"+name);
        
        tmpObj.transform.SetParent(pauseMenu.transform, false);
        // Set its position in world space
        //tmpObj.transform.position = new Vector3(0, textYPos, 0);

        // Add TextMeshPro component
        TextMeshProUGUI tmp = tmpObj.AddComponent<TextMeshProUGUI>();

        tmp.text = name;
        tmp.fontSize = 36;
        tmp.color = Color.white;
        tmp.alignment = TextAlignmentOptions.Center;
        RectTransform rect = tmp.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, textYPos); // Top center
        rect.sizeDelta = new Vector2(884, 50);
        objectiveObjectList.Add(tmpObj);
    }
}
