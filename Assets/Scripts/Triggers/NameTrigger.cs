using TMPro;
using UnityEngine;

public class NameTrigger : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject inputFieldObject;
    
    public void TurnInputFieldOn()
    {
        inputFieldObject.SetActive(true);
    }
    
    public void SendName()
    {
        if (true) // nome valido
        {
            
        }
        else
        {
            // Enviar um dialogo unico
        }
    }
}
