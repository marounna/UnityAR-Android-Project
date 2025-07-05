using UnityEngine;
using System.Collections;
public class PlayNarrationOnEnable : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayed = false;

    private SubtitleManager subtitleManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        if (audioSource != null && !hasPlayed)
        {
            StartCoroutine(MyWaitRoutine());
            audioSource.Play();
            hasPlayed = true;

            // Find and show subtitles
            subtitleManager = FindObjectOfType<SubtitleManager>();
            if (subtitleManager != null)
            {
                subtitleManager.ShowSubtitle();
            }
        }


    }
    IEnumerator MyWaitRoutine()
    {
        Debug.Log("Waiting 3 seconds...");
        yield return new WaitForSeconds(3f);
        Debug.Log("Done waiting!");
    }
}
