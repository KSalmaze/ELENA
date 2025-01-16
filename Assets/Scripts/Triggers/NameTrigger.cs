using System.Linq;
using TMPro;
using UnityEngine;

public class NameTrigger : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject inputFieldObject;
    [SerializeField] private Dialogue npc;
    [SerializeField] private Variables variables;
    [SerializeField] private int min = 2;
    [SerializeField] private int max = 30;

    [Header("Sentences")] 
    [SerializeField] private string noNameSentence;
    [SerializeField] private string onlySpaceSentence;
    [SerializeField] private string hasNumbersSentence;
    [SerializeField] private string especialCharSentence;
    [SerializeField] private string shortNameSentence;
    [SerializeField] private string longNameSentence;
    
    public void TurnInputFieldOn()
    {
        inputFieldObject.SetActive(!inputFieldObject.activeSelf);
    }
    
    public void SendName()
    {
        string playerName = inputField.text;
        
        if (playerName == string.Empty)
        {
            npc.UniqueDialogue(noNameSentence);
        }
        else if (string.IsNullOrWhiteSpace(playerName))
        {
            npc.UniqueDialogue(onlySpaceSentence);
        }
        else if (playerName.Any(char.IsDigit))
        {
            npc.UniqueDialogue(hasNumbersSentence);
        }
        else if (playerName.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
        {
            npc.UniqueDialogue(especialCharSentence);
        }
        else if (playerName.Length < min)
        {
            npc.UniqueDialogue(shortNameSentence);
        }
        else if (playerName.Length > max)
        {
            npc.UniqueDialogue(longNameSentence);
        }
        else
        {
            variables.SetVariable("name", playerName);
            npc.GoToNextDialogue();
        }
    }
}
