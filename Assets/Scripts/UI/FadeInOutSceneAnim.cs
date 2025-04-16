using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInOutSceneAnim : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    private void Start()
    {
        
            fadeCanvasGroup.alpha = 0f; 
    }

    public void FadeIn()  
    {
        StartCoroutine(Fade(0f, 1f));
    }

    public void FadeOut() 
    {
        StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeCanvasGroup.alpha = alpha;
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;
    }
}
