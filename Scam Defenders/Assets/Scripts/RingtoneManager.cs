using UnityEngine;

public class RingtoneManager : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip audioClip;      // The audio clip to play
    public GameObject[] stopButtons;    // The buttons to stop the audio

    private void OnEnable()
    {
        PlayAudio();
    }

    private void OnDisable()
    {
        StopAudio();
    }

    private void PlayAudio()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }
    }

    private void Start()
    {
        foreach (var button in stopButtons)
        {
            if (button != null)
            {
                button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(StopAudio);
            }
        }
    }
}
