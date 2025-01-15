using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    [SerializeField] private AnimationClip openingClip;
    
    void Start()
    {
        Resolution currentResolution = Screen.currentResolution;
        
        int halfWidth = currentResolution.width / 2;
        int halfHeight = currentResolution.height;
        
        Screen.SetResolution(halfWidth, halfHeight, false);

        StartCoroutine(GotoMenu());
    }

    IEnumerator GotoMenu()
    {
        Debug.Log(openingClip.length);
        yield return new WaitForSeconds(openingClip.length);
        SceneManager.LoadScene("MainMenu");
    }
}
