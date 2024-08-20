using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

[System.Serializable]
public class DialogueSegment
{
    public string[] lines;
    public AudioClip[] audioClips;
    public float[] lineDurations;
    public Button triggerButton;  // Button to trigger this segment
    public GameObject[] objectsToActivate;  // Objects to activate after dialogue
    public GameObject[] objectsToDeactivate;  // Objects to deactivate after dialogue
}

public class ScamCallManager : MonoBehaviour
{
    public YabberData yabberdata;
    public AudioSource audioSource;
    public TextMeshProUGUI scamText;
    public DialogueSegment[] dialogueSegments;  // Array of dialogue segments

    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject scamCall;
    public GameObject declineNotification;  // Reference to the notification GameObject in the scene
    public GameObject YabberPing;

    void Start()
    {
        // Assign listeners to each segment's button
        for (int i = 0; i < dialogueSegments.Length; i++)
        {
            int index = i;  // Capture the index for the lambda expression
            dialogueSegments[i].triggerButton.onClick.AddListener(() => StartDialogueSegment(index));
        }
    }

    void StartDialogueSegment(int segmentIndex)
    {
        if (segmentIndex < dialogueSegments.Length)
        {
            StartCoroutine(PlayDialogueSegment(segmentIndex));
        }
    }

    IEnumerator PlayDialogueSegment(int segmentIndex)
    {
        scamText.gameObject.SetActive(true);

        string[] lines = dialogueSegments[segmentIndex].lines;
        AudioClip[] clips = dialogueSegments[segmentIndex].audioClips;
        float[] durations = dialogueSegments[segmentIndex].lineDurations;

        for (int i = 0; i < lines.Length; i++)
        {
            scamText.text = lines[i];
            audioSource.clip = clips[i];
            audioSource.Play();
            yield return new WaitForSeconds(durations[i]);
        }

        scamText.text = ""; // Clear text after the segment ends
        scamText.gameObject.SetActive(false);

        // Activate the specified game objects
        foreach (GameObject obj in dialogueSegments[segmentIndex].objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        // Deactivate the specified game objects
        foreach (GameObject obj in dialogueSegments[segmentIndex].objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    // Method to handle call decline
    public void DeclineCall()
    {
        scamCall.SetActive(false);

        if (declineNotification != null)
        {
            declineNotification.SetActive(true);  // Activate the decline notification
            YabberPing.SetActive(true);
            yabberdata.isGOISClicked = true;
        }
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
    }

    public void Win()
    {
        loseScreen.SetActive(true);
    }
}
