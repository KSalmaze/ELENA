using System.IO;
using UnityEngine;

namespace Puzzles
{
    public class FindTheFile : MonoBehaviour
    {
        [SerializeField] private string chosenPath;
        
        void Start()
        {
            ChoosePath();
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
            else
            {
                chosenPath = @"C:\Program Files (x86)";
            }
            
            Debug.Log(chosenPath);
        }
    }
}
