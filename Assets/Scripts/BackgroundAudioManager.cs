using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void ToggleMute()
    {
        //audioSource.mute = !audioSource.mute;
        AudioListener.pause = !AudioListener.pause;
    }

    public void PlayClick()
    {
        // If muted, don't play click
        if (clickSound != null && !audioSource.mute)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
    public void PlayFeedbackSound(bool isCorrect)
    {
        if (audioSource.mute) return;

        if (isCorrect && correctSound != null)
        {
            audioSource.PlayOneShot(correctSound);
        }
        else if (!isCorrect && wrongSound != null)
        {
            audioSource.PlayOneShot(wrongSound);
        }
    }
}
