using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;

    [TextArea]
    public string englishSubtitle;
    [TextArea]
    public string italianSubtitle;
    [TextArea]
    public string spanishSubtitle;

    public float displayDuration = 10f;

    private Coroutine hideCoroutine;

    public void ShowSubtitle()
    {
        UpdateSubtitleText();

        subtitleText.gameObject.SetActive(true);

        // Start timer to hide
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideAfterSeconds(displayDuration));
    }

    public void UpdateSubtitleText()
    {
        string selected = LanguageManager.SelectedLanguage;

        if (selected == "English")
        {
            subtitleText.text = englishSubtitle;
        }
        else if (selected == "Italian")
        {
            subtitleText.text = italianSubtitle;
        }
        else if (selected == "Spanish")
        {
            subtitleText.text = spanishSubtitle;
        }
        else
        {
            subtitleText.text = "";
        }
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        subtitleText.gameObject.SetActive(false);
    }
}
