using Puzzles;
using UnityEngine;

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
        variables.SetVariable("arq1", fileManager.currentDirectory + "/" + fileFolder);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
