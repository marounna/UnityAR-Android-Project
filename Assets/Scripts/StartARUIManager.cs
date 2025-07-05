using UnityEngine;
using System.Collections;

public class StartARUIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject scanInstructionsPanel;
    public GameObject ExitButton;

    public float fadeDuration = 1.0f;   // Time to fade in/out
    public float displayDuration = 3.0f; // Time to stay fully visible

    private CanvasGroup scanCanvasGroup;

    void Start()
    {
        scanCanvasGroup = scanInstructionsPanel.GetComponent<CanvasGroup>();
        scanCanvasGroup.alpha = 0f;
        scanInstructionsPanel.SetActive(false);
    }

    public void StartAR()
    {
        mainMenuPanel.SetActive(false);
        scanInstructionsPanel.SetActive(true);
        scanCanvasGroup.alpha = 0f;

        // Start full sequence
        StartCoroutine(FadeInOutSequence());
        ExitButton.SetActive(true);
    }

    private IEnumerator FadeInOutSequence()
    {
        // Fade in
        yield return StartCoroutine(FadeCanvasGroup(scanCanvasGroup, 0f, 1f, fadeDuration));

        // Wait while fully visible
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        yield return StartCoroutine(FadeCanvasGroup(scanCanvasGroup, 1f, 0f, fadeDuration));

        // Optionally deactivate panel after fade out
        scanInstructionsPanel.SetActive(false);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        canvasGroup.alpha = startAlpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }

     public void ExitToMenu()
    {
        // Show the main menu buttons again
        mainMenuPanel.SetActive(true);

        // Hide scan instructions (immediately)
        scanInstructionsPanel.SetActive(false);
        ExitButton.SetActive(false);
    }
}
