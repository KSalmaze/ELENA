using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;

namespace Puzzles
{
    public class FindTheFile : MonoBehaviour
    {
        [SerializeField] private GameObject puzzleCanvas;
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private GameObject tilesParent;
        [SerializeField] private Dialogue npc;
        [SerializeField] private Variables variables;
        public bool goNext = false;

        [Header("Secret File")] 
        [SerializeField] private FileManager fileManager;
        [SerializeField] private string zipName;
        
        [Header("Debug")]
        [SerializeField] private string chosenPath;
        [SerializeField] private List<string> path;
        
        void Start()
        {
            ChoosePath();
            variables.SetVariable("arq3", chosenPath);
            path = DividirCaminho(chosenPath);
        }

        public void Unzips()
        {
            if (Directory.Exists(chosenPath + "/Pasta_Secreta_OwO"))
            {
                Directory.Delete(chosenPath + "/Pasta_Secreta_OwO", true);
            }
            
            string zipPath = Path.Combine(Application.dataPath, zipName);
            string extractPath = chosenPath;
            
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

        private void ChoosePath()
        {
            if (Directory.Exists(@"C:\Program Files (x86)\Steam\steamapps\common")) // Steam
            {
                string[] pastas = Directory.GetDirectories(@"C:\Program Files (x86)\Steam\steamapps\common");

                if (pastas.Length != 0)
                {
                    int indiceAleatorio = Random.Range(0, pastas.Length);
                    chosenPath = pastas[indiceAleatorio];
                }
                else
                {
                    chosenPath = @"C:\Program Files (x86)\Steam\steamapps\common";
                }
            }else if (Directory.Exists(@"C:\Program Files\Epic Games")) // Epic Games
            {
                string[] pastas = Directory.GetDirectories(@"C:\Program Files\Epic Games");

                if (pastas.Length != 0)
                {
                    int indiceAleatorio = Random.Range(0, pastas.Length);
                    chosenPath = pastas[indiceAleatorio];
                }
                else
                {
                    chosenPath = @"C:\Program Files\Epic Games";
                }
            }
            else if(Directory.Exists(@"C:\Program Files (x86)"))
            {
                chosenPath = @"C:\Program Files (x86)";
            }
            else
            {
                chosenPath = "C:";
            }
            
            Debug.Log(chosenPath);
        }
        
        private List<string> DividirCaminho(string caminho)
        {
            List<string> partes = new List<string>();
            string diretorio = caminho;
    
            // Adiciona o drive primeiro (ex: "C:")
            string drive = Path.GetPathRoot(diretorio);
            if (!string.IsNullOrEmpty(drive))
            {
                partes.Add(drive);
            }
    
            // Divide o resto do caminho
            string[] pedacos = diretorio.Replace(drive, "").Split(Path.DirectorySeparatorChar);
            foreach (string pedaco in pedacos)
            {
                if (!string.IsNullOrEmpty(pedaco))
                {
                    partes.Add(Path.DirectorySeparatorChar + pedaco);
                }
            }
    
            return partes;
        }

        public void StartPuzzle()
        {
            StartCoroutine(puzzleCoroutine());
        }

        private IEnumerator puzzleCoroutine()
        {
            string currentPath = path[0];
            for (int i = 1; i < path.Count; i++)
            {
                GenButtons(currentPath,currentPath + path[i]);
                currentPath += path[i];
                while (!goNext){yield return new WaitForSeconds(0.1f);}
                goNext = false;
            }
            
            tilesParent.SetActive(false);
            
            npc.GoToNextDialogue();

            yield return null;
        }
        
        private void GenButtons(string _path, string next_path)
        {
            foreach (Transform child in tilesParent.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (string folder in Directory.GetDirectories(_path))
            {
                GameObject button = Instantiate(tilePrefab, tilesParent.transform);
                button.GetComponent<InButton>().Initialize(this, folder, next_path);
                button.transform.localScale = Vector3.one;
            }
        }
    }
}
