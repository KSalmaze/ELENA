using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class AI_Dialog : MonoBehaviour
{
    [SerializeField] private float letterTransitionTime = 0.06f;
    [SerializeField] private TMP_Text elenaBox;
    [SerializeField] private TMP_Text seresBox;
    [SerializeField] private float dialogueDelay = 0.3f;
    [SerializeField] private List<string> dialogues;
    private bool next = false;

    void Start()
    {
        StartCoroutine(Dialogue());
    }
    
    IEnumerator Dialogue()
    {
        foreach (string di in dialogues)
        {
            next = false;
            char npc = di[0];
            string fala = di.Substring(2);
            
            if (npc == 'S')
            {
                StartCoroutine(UpdateTextBox(fala,seresBox));
            }
            else
            {
                StartCoroutine(UpdateTextBox(fala,elenaBox));
            }

            while (!next)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    
    IEnumerator UpdateTextBox(string sentence, TMP_Text textBox)
    {
        textBox.text = string.Empty;
        
        StringBuilder sb = new StringBuilder(textBox.text);
        for (int i = 0; i < sentence.Length; i++)
        {
            sb.Append(sentence[i]);
            textBox.text = sb.ToString();
            yield return new WaitForSeconds(letterTransitionTime);
        }
        
        yield return new WaitForSeconds(dialogueDelay);
        next = true;
    }
}
