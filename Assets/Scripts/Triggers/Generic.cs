using TMPro;
using UnityEngine;

public class Generic : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private string expectedInput;
    [SerializeField] private Dialogue npc;
    
    public void SwitchGameObjectState(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void SubmitInput()
    {
        if (inputField.text == expectedInput)
        {
            npc.GoToNextDialogue();
        }
        else
        {
            npc.UniqueDialogue("Não funcionou, veja direito");
        }
    }
}
