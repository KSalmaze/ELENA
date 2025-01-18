using System.Collections;
using System.IO;
using UnityEngine;

namespace Puzzles
{
    public class DeleteAFile : MonoBehaviour
    {
        [SerializeField] private FileManager fileManager;
        [SerializeField] private Dialogue npc;
        [SerializeField] private Variables variables;

        [SerializeField] private string fileFolder;
        [SerializeField] private string fileName;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            variables.SetVariable("arq1", fileManager.gameplayDirectory + "/" + fileFolder);
            string file = fileManager.gameplayDirectory + "/" + fileFolder + "/" + fileName;
            Debug.Log($"Verficiar aquivo: {file}");
        }

        public void CheckForFileDelete()
        {
            StartCoroutine(MonitorarArquivo());
        }
    
        private IEnumerator MonitorarArquivo()
        {
            string filePath = fileManager.gameplayDirectory + "/" + fileFolder + "/" + fileName;
            
            while (true)
            {
                if (!File.Exists(filePath))
                {
                    Debug.Log("Arquivo deletado");
                    npc.GoToNextDialogue();
                    yield break;
                }

                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
