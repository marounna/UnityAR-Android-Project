using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LocalizedTextManager : MonoBehaviour
{
    [System.Serializable]
    public class LocalizedTextEntry
    {
        public string key; // e.g., "StartButton"
        public string englishText;
        public string italianText;
        public string spanishText;
        public TextMeshProUGUI targetText;
    }

    public List<LocalizedTextEntry> localizedTexts;

    public void UpdateAllTexts()
    {
        string selected = LanguageManager.SelectedLanguage;

        foreach (var entry in localizedTexts)
        {
            if (entry.targetText == null) continue;

            if (selected == "English")
                entry.targetText.text = entry.englishText;
            else if (selected == "Italian")
                entry.targetText.text = entry.italianText;
            else if (selected == "Spanish")
                entry.targetText.text = entry.spanishText;
        }
    }
}
