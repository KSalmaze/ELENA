using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[RequireComponent(typeof(Triggers))]
[DisallowMultipleComponent]
public class Dialogue : MonoBehaviour
{
    [SerializeField] private string separationCharacter = "%";
    [SerializeField] private string repetitiveCharacter = "/";
    [SerializeField] private List<string> sentences;
    [SerializeField] private TMP_Text textBox;
    [SerializeField] public GameObject endOfSentenceIndicator;
    [SerializeField] private float LetterTransitionTime = 0.2f;

    private int _pointer = 0; // Aponta para a sentenca atual
    private Coroutine showTextCorotine;

    private void Start()
    {
        showTextCorotine =  StartCoroutine(UpdateTextBox(sentences[_pointer]));   
    }

    public void NextSentence()
    {
        string nextSentence = sentences[_pointer + 1];
        
        if (nextSentence == separationCharacter)
        {
            // FIM DE SENQUENCIA DE DIALOGO
        } 
        else if (nextSentence == repetitiveCharacter)
        {
            // ENTRAR NO LOOP DE DIALOGO
        }
        else
        {
            _pointer++;
            showTextCorotine =  StartCoroutine(UpdateTextBox(nextSentence));
        }
    }

    IEnumerator UpdateTextBox(string sentence)
    {
        textBox.text = string.Empty;
        
        foreach (char letter in sentence)
        {
            textBox.text = textBox.text + letter;
            yield return new WaitForSeconds(LetterTransitionTime);
        }
    }
}
