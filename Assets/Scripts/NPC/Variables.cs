using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    [SerializeField] private List<string> variableName;
    [SerializeField] private List<string> variableValue;

    public string GetVariable(string varName)
    {
        if (variableName.Contains(varName))
        {
            return variableValue[variableName.IndexOf(varName)];
        }
        
        return $"ERROR in var {varName}";
    }

    public void SetVariable(string varName, string value)
    {
        variableName.Add(varName);
        variableValue.Add(value);
    }
}
