using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

[RequireComponent(typeof(TriggersManager))]
[RequireComponent(typeof(Variables))]
[DisallowMultipleComponent]
public class Dialogue : MonoBehaviour
{
    // Arrumar os dialogos Unicos quando apertar pra completar
    // Adicionar a flag de nome no passar mensagem
    // Quando pular msg verificar se a msg é %
    
    [SerializeField] private string separationCharacter = "%";
    [SerializeField] private string repetitiveCharacter = "/";
    [SerializeField] private char triggerCharacter = '@';
    [SerializeField] private List<string> sentences;
    [SerializeField] private TMP_Text textBox;
    [SerializeField] public GameObject endOfSentenceIndicator;
    [SerializeField] private float letterTransitionTime = 0.2f;
    [SerializeField] private TriggersManager triggersManager;
    [SerializeField] private Variables variables;
    
    [SerializeField] private int pointer = 0; // Aponta para a sentenca atual
    private Coroutine _showTextCoroutine;

    private void Start()
    {
        _showTextCoroutine =  StartCoroutine(UpdateTextBox(sentences[pointer]));   
    }

    public void NextSentence()
    {
        if (_showTextCoroutine != null)
        {
            StopCoroutine(_showTextCoroutine);
            _showTextCoroutine = null;

            if (sentences[pointer][0] == '+')
            {
                textBox.text = string.Empty;
                textBox.text = sentences[pointer - 1] + sentences[pointer].Substring(1);
            }
            else if (sentences[pointer].Contains('-'))
            {
                UpdateTextBoxNoDelay(sentences[pointer]);
            }else
            {
                textBox.text = string.Empty;
                textBox.text = sentences[pointer];
            }
            return;
        }
        
        string nextSentence = sentences[pointer + 1];

        if (nextSentence[0] == triggerCharacter)
        {
            string[] parts = nextSentence.Split(triggerCharacter);
            triggersManager.Trigger(parts[1], parts[0]);
            pointer += 1;
            nextSentence = sentences[pointer + 1];
        }
        
        if (nextSentence == separationCharacter)
        {
            for (int i = pointer; i >= 0 ; i--)
            {
                if (sentences[i] == repetitiveCharacter)
                {
                    pointer = i - 1;
                    NextSentence();
                    return;
                }
            }
        } 
        else if (nextSentence == repetitiveCharacter)
        {
            pointer += 2;
            _showTextCoroutine = StartCoroutine(UpdateTextBox(sentences[pointer]));
        }
        else
        {
            pointer++;
            _showTextCoroutine =  StartCoroutine(UpdateTextBox(nextSentence));
        }
    }

    public void GoToNextDialogue()
    {
        int temp = pointer;
        while (sentences[temp] != separationCharacter)
        {
            Debug.Log(sentences[temp]);
            temp++;
        }
        
        pointer = temp;
        NextSentence();
    }

    public void UniqueDialogue(string sentence)
    {
        if (_showTextCoroutine != null)
        {
            StopCoroutine(_showTextCoroutine);
            _showTextCoroutine = null;
        }
        
        // Se sim interomper e e depois falar "voltando ao que estava dizendo"
        
        _showTextCoroutine =  StartCoroutine(UpdateTextBox(sentence));
    }

    private void UpdateTextBoxNoDelay(string sentence)
    {
        textBox.text = string.Empty;
        StringBuilder sb = new StringBuilder(textBox.text);
        for (int i = 0; i < sentence.Length; i++)
        {
            textBox.text += String.Empty;
            if (sentence[i] == '-')
            {
                int numeroDeLetras = sentence[i + 1] - '0';
                for (int j = 0; j < numeroDeLetras; j++)
                {
                    sb.Remove(sb.Length - 1, 1); // remover o ultimo caractere
                    textBox.text = sb.ToString();
                }

                i += 2;;
            }
            
            if (i < sentence.Length && sentence[i] == '*')
            {
                int inicio = i;
        
                inicio++; // Pula o primeiro asterisco
                int fim = sentence.IndexOf("*", inicio, StringComparison.Ordinal);
                
                string chave = sentence.Substring(inicio, fim - inicio);
                
                Debug.Log($"Acessando variavel {chave}");

                sentence = sentence.Replace("*" + chave + "*", variables.GetVariable(chave));
            }
            
            sb.Append(sentence[i]);
            textBox.text = sb.ToString();
        }
    }
    
    IEnumerator UpdateTextBox(string sentence)
    {
        if (sentence[0] == '+')
        {
            sentence = sentence.Substring(1);
        }
        else
        {
            textBox.text = string.Empty;
        }
        
        StringBuilder sb = new StringBuilder(textBox.text);
        for (int i = 0; i < sentence.Length; i++)
        {
            if (sentence[i] == '-')
            {
                int numeroDeLetras = sentence[i + 1] - '0';
                for (int j = 0; j < numeroDeLetras; j++)
                {
                    sb.Remove(sb.Length - 1, 1); // remover o ultimo caractere
                    textBox.text = sb.ToString();
                    yield return new WaitForSeconds(letterTransitionTime + 0.01f);
                }

                i += 2;;
            }

            if (i < sentence.Length && sentence[i] == '*')
            {
                int inicio = i;
        
                inicio++; // Pula o primeiro asterisco
                int fim = sentence.IndexOf("*", inicio, StringComparison.Ordinal);
                
                string chave = sentence.Substring(inicio, fim - inicio);
                
                Debug.Log($"Acessando variavel {chave}");

                sentence = sentence.Replace("*" + chave + "*", variables.GetVariable(chave));
            }
            
            sb.Append(sentence[i]);
            textBox.text = sb.ToString();
            yield return new WaitForSeconds(letterTransitionTime);
        }
        
        _showTextCoroutine = null;
    }
}
