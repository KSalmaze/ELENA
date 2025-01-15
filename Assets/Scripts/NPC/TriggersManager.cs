using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggersManager : MonoBehaviour
{
    [SerializeField] public List<string> triggerName;
    [SerializeField] public UnityEvent<string> stringEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
