using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Image imageToFade;

    void Start()
    {
        StartCoroutine(FadeOut());
    }
    
    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color originalColor = imageToFade.color;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            
            imageToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        
        imageToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        imageToFade.enabled = false;
    }
}
