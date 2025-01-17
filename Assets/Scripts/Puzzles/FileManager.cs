using System.IO;
using System.IO.Compression;
using UnityEngine;

namespace Puzzles
{
    public class FileManager : MonoBehaviour
    {
        [SerializeField] private string zipFileName;
        [SerializeField] private string gameplayFolderName;
        [SerializeField] private Dialogue npc;
        [SerializeField] private GameObject fileErrorCanvas;
        
        [Header("Public variables")]
        public string currentDirectory;
        public string gameplayDirectory;
        public string targetFilePath;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentDirectory = Application.dataPath;
            CreateGameplayDirectory();
            
            // Verificar se o zip existe, se não dar erro
        }

        private void CreateGameplayDirectory()
        {
            if (Directory.Exists(gameplayFolderName))
            {
                Directory.Delete(gameplayFolderName, true);
            }
            
            string zipPath = Path.Combine(Application.dataPath, zipFileName);
            string extractPath = Path.Combine(Application.dataPath, currentDirectory);
            
            try
            {
                ZipFile.ExtractToDirectory(zipPath, extractPath);
                Debug.Log("Arquivos extraídos");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Erro ao extrair arquivos: " + e.Message);
            }
        }
        
        private void CreateTargetFile()
        {
            // Verificar se a pasta steam existe
            // Se não verificar a pasta epic games
            // Se não colocar na pasta de documentos
        }

        /*IEnumerator CheckGameDirectory()
        {
            // Checa se algo mudou no diretório, verifica o esperado
            // Se a mundança não for esperada manda a npc falar algo
            //      apaga e recria o diretório
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
