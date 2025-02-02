using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Puzzles
{
    public class ModifyAFile : MonoBehaviour
    {
        [SerializeField] private FileManager fileManager;
        [SerializeField] private Dialogue npc;
        [SerializeField] private Variables variables;
        
        [SerializeField] private string fileFolder;
        [SerializeField] private string fileName;
        
        [Header("File info")]
        [SerializeField] List<string> fileInitialState;
        [SerializeField] List<string> expectedFileState;
        [Header("Expected changes")]
        [SerializeField] List<int> line;
        [SerializeField] List<string> expectedLine;

        void Start()
        {
            string file = fileManager.gameplayDirectory + "/" + fileFolder + "/" + fileName;
            Debug.Log($"Verficiar aquivo: {file}");
            variables.SetVariable("arq2", fileManager.gameplayDirectory + "/" + fileFolder);
            
            fileInitialState = new List<string>(File.ReadAllLines(file));
            expectedFileState = new List<string>(File.ReadAllLines(file));

            for (int i = 0; i < line.Count; i++)
            {
                expectedFileState[line[i]] = expectedLine[i];
            }
        }

        public void CheckForFileWrite()
        {
            StartCoroutine(StartCheck());
        }

        private IEnumerator StartCheck()
        {
            string file = fileManager.gameplayDirectory + "/" + fileFolder + "/" + fileName;
            DateTime ultimaModificacao = File.GetLastWriteTime(file);

            while (true)
            {
                if (ultimaModificacao != File.GetLastWriteTime(file))
                {
                    string[] lines = File.ReadAllLines(file);
                    if (CheckIfFileState(fileInitialState, lines))
                    {
                        npc.UniqueDialogue("Ué, você alterou o arquivo mas não mudou nada?");
                    }else if (CheckIfFileState(expectedFileState, lines))
                    {
                        npc.GoToNextDialogue();
                    }
                    else
                    {
                        npc.UniqueDialogue("A que bom, você alterou o arquivo e mexeu no que não devia, " +
                                           "agora as coisas aqui dentro não param de apitar, vou resolver isso");
                        // Resolver isso
                    }
                    
                    ultimaModificacao = File.GetLastWriteTime(file);
                }
                
                yield return new WaitForSeconds(0.2f);
            }
        }

        private bool CheckIfFileState(List<string> state, string[] file)
        {
            for (int i = 0; i < state.Count; i++)
            {
                Debug.Log(state[i]);
                Debug.Log(file[i]);
                Debug.Log(state[i] != file[i]);
                if (state[i] != file[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
