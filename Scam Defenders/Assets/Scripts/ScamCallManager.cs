using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class DialogueStep
{
    public string text;
    public AudioClip audioClip;
    public float duration;
    public Button actionButton;       // Optional: Button to display for this step
    public GameObject[] gameObjectsToActivate; // Optional: Game objects to activate at this step
}

public class ScamCallManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI scamText;
    public List<DialogueStep> dialogueSteps = new List<DialogueStep>(); // List of dialogue steps

    private int currentStep = 0;

    void Start()
    {
        // Initially hide all action buttons and deactivate game objects
        foreach (var step in dialogueSteps)
        {
            if (step.actionButton != null)
            {
                step.actionButton.gameObject.SetActive(false);
            }
            foreach (var go in step.gameObjectsToActivate)
            {
                go.SetActive(false);
            }
        }

        StartCoroutine(PlayScamCall());
    }

    IEnumerator PlayScamCall()
    {
        while (currentStep < dialogueSteps.Count)
        {
            DialogueStep step = dialogueSteps[currentStep];

            scamText.text = step.text;
            audioSource.clip = step.audioClip;
            audioSource.Play();

            yield return new WaitForSeconds(step.duration);

            // Activate any game objects associated with this step
            foreach (var go in step.gameObjectsToActivate)
            {
                go.SetActive(true);
            }

            // Check if there's an action button for this step
            if (step.actionButton != null)
            {
                audioSource.Stop();  // Stop audio while waiting for interaction
                step.actionButton.gameObject.SetActive(true);
                step.actionButton.onClick.RemoveAllListeners();
                step.actionButton.onClick.AddListener(ResumeScamCall);
                yield break;  // Exit the coroutine to wait for the button press
            }

            currentStep++;  // Move to the next step
        }

        scamText.text = ""; // Clear text after the dialogue ends
    }

    void ResumeScamCall()
    {
        // Hide the action button when resuming
        if (dialogueSteps[currentStep].actionButton != null)
        {
            dialogueSteps[currentStep].actionButton.gameObject.SetActive(false);
        }

        currentStep++;  // Move to the next step after the button press
        StartCoroutine(PlayScamCall());  // Resume the call
    }
}
