using UnityEngine;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;

    public static string SelectedLanguage = "English";

    void Start()
    {
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        // Set default language
        SelectedLanguage = languageDropdown.options[languageDropdown.value].text;
    }

    void OnLanguageChanged(int index)
    {
        SelectedLanguage = languageDropdown.options[index].text;
        Debug.Log("Language set to: " + SelectedLanguage);

        // Update subtitles if they are visible
        var subtitleManager = FindObjectOfType<SubtitleManager>();
        if (subtitleManager != null && subtitleManager.subtitleText.gameObject.activeSelf)
        {
            subtitleManager.UpdateSubtitleText();
        }

            // Update all UI texts
        var textManager = FindObjectOfType<LocalizedTextManager>();
        if (textManager != null)
        {
            textManager.UpdateAllTexts();
        }
    }

}
