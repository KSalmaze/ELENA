using TMPro;
using UnityEngine;

public class TriggerPerguntas : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private TMP_InputField textBox;
    [SerializeField] private Dialogue npc;
    public void SwitchOnAndOff()
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void SendQuestion()
    {
        if (string.IsNullOrWhiteSpace(textBox.text))
        {
            npc.UniqueDialogue("Eu preciso que você digite alguma coisa para poder respoder >:(");
        }
        else if (!textBox.text.Contains('?'))
        {
            npc.UniqueDialogue("Perdão mas para meus sistema identificarem uma pergunta a frase precisa conter o caractere '?'");
        }
        else
        {
            npc.GoToNextDialogue();
        }
    }
}
