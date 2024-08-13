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
    public AudioSource audioSource;
    public TextMeshProUGUI scamText;
    public DialogueSegment[] dialogueSegments;  // Array of dialogue segments

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
        // Optionally disable the button after pressing
        //dialogueSegments[segmentIndex].triggerButton.gameObject.SetActive(false);

        // Activate scamText when dialogue starts
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

        // Clear the text and deactivate scamText after dialogue ends
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
}