using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggersManager : MonoBehaviour
{
    [SerializeField] public List<string> triggerNames;
    [SerializeField] public List<UnityEvent<string>> events;

    public void Trigger(string triggerName, string parameter)
    {
        Debug.Log($"trigger: {triggerName}, parameter: {parameter}");
        int eventIndex = triggerNames.IndexOf(triggerName);
        events[eventIndex].Invoke(parameter);
    }
}
