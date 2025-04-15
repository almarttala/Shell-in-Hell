using System;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public string name;
    public bool isComplete = false;

    //trigger condition
    public System.Func<bool> triggerCondition;

    //on complete trigger: executes when objective is completed
    public Action onCompleteTrigger;


    public Objective(string name, System.Func<bool> trigger = null, Action onComplete = null)
    {
        this.name = name;
        this.triggerCondition = trigger;
        this.onCompleteTrigger = onComplete;
    }

    public void CheckIfCompleted()
    {
        if (triggerCondition != null && triggerCondition())
        {
            Complete();
        }
    }
    public void Complete()
    {
        if (!isComplete)
        {
            isComplete = true;
            if (onCompleteTrigger != null)
            {
                onCompleteTrigger?.Invoke();
               
            }
        }
    }
}
