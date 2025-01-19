using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    [SerializeField] private AnimationClip openingClip;
    [SerializeField] private float waitTime;
    
    void Start()
    {
        Resolution currentResolution = Screen.currentResolution;
        
        int halfWidth = currentResolution.width / 2;
        int halfHeight = currentResolution.height;
        
        Screen.SetResolution(halfWidth, halfHeight - 100, false);

        StartCoroutine(GotoMenu());
    }

    IEnumerator GotoMenu()
    {
        Debug.Log(openingClip.length);
        yield return new WaitForSeconds(openingClip.length - waitTime);
        SceneManager.LoadScene("MainMenu");
    }
}
