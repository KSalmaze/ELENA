using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

//[RequireComponent(typeof(Triggers))]
[DisallowMultipleComponent]
public class Dialogue : MonoBehaviour
{
    [SerializeField] private string separationCharacter = "%";
    [SerializeField] private string repetitiveCharacter = "/";
    [SerializeField] private List<string> sentences;
    [SerializeField] private TMP_Text textBox;
    [SerializeField] public GameObject endOfSentenceIndicator;
    [SerializeField] private float letterTransitionTime = 0.2f;

    private int _pointer = 0; // Aponta para a sentenca atual
    private Coroutine _showTextCorotine;

    private void Start()
    {
        _showTextCorotine =  StartCoroutine(UpdateTextBox(sentences[_pointer]));   
    }

    public void NextSentence()
    {
        if (_showTextCorotine != null)
        {
            StopCoroutine(_showTextCorotine);
            _showTextCorotine = null;
            textBox.text = string.Empty;
            textBox.text = sentences[_pointer];
            return;
        }
        
        string nextSentence = sentences[_pointer + 1];
        
        if (nextSentence == separationCharacter)
        {
            for (int i = _pointer; i >= 0 ; i--)
            {
                if (sentences[i] == repetitiveCharacter)
                {
                    _pointer = i - 1;
                    NextSentence();
                    return;
                }
            }
        } 
        else if (nextSentence == repetitiveCharacter)
        {
            _pointer += 2;
            _showTextCorotine = StartCoroutine(UpdateTextBox(sentences[_pointer]));
        }
        else
        {
            _pointer++;
            _showTextCorotine =  StartCoroutine(UpdateTextBox(nextSentence));
        }
    }

    public void GoToNextDialogue()
    {
        int temp = _pointer;
        while (sentences[temp] != separationCharacter)
        {
            Debug.Log(sentences[temp]);
            temp++;
        }
        
        _pointer = temp;
        NextSentence();
    }
    
    IEnumerator UpdateTextBox(string sentence)
    {
        textBox.text = string.Empty;
        
        foreach (char letter in sentence)
        {
            textBox.text = textBox.text + letter;
            yield return new WaitForSeconds(letterTransitionTime);
        }
        
        _showTextCorotine = null;
    }
}
