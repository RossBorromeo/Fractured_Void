using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image fadeImage;
    public float fadeDuration = 1f;
    public float fadeHoldTime = 0.5f; // How long to stay black before fading back in

    void Awake()
    {
        if (fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 0); // Start transparent
    }

    public void FadeOutIn(System.Action onFadeMid = null)
    {
        StartCoroutine(FadeRoutine(onFadeMid));
    }

    private IEnumerator FadeRoutine(System.Action onFadeMid)
    {
        yield return StartCoroutine(Fade(0f, 1f)); // Fade to black

        onFadeMid?.Invoke(); // Run logic while screen is black

        yield return new WaitForSeconds(fadeHoldTime); // Stay black for a moment

        yield return StartCoroutine(Fade(1f, 0f)); // Fade back to transparent
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
