using System.Collections;
using UnityEngine;

namespace Puzzles
{
    public class FileManager : MonoBehaviour
    {
        public string currentPath;
        public string targetFilePath;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentPath = Application.dataPath;
        }

        private void CreateGameplayDirectory()
        {
            // Verificar se já existe
            
        }
        
        private void CreateTargetFile()
        {
            // Verificar se a pasta steam existe
            // Se não verificar a pasta epic games
            // Se não colocar na pasta de documentos
        }

        /*IEnumerator CheckGameDirectory()
        {
            
        }

        IEnumerator CheckTargetFile()
        {
            
        }*/
        
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
