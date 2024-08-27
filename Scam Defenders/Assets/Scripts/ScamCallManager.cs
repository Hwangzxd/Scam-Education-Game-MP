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

[System.Serializable]
public class Scenario
{
    public string scenarioName;  // Name of the scenario
    public DialogueSegment[] dialogueSegments;  // Dialogue segments for this scenario
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject loseScreen1;
    public GameObject winScreen1;
    public GameObject scamCall;
    public GameObject declineNotification;
    public GameObject YabberPing;
}

public class ScamCallManager : MonoBehaviour
{
    public YabberData yabberdata;
    public AudioSource audioSource;
    public TextMeshProUGUI scamText;
    public Scenario[] scenarios;  // Array of scenarios
    private Scenario currentScenario;  // Reference to the currently active scenario

    void Start()
    {
        // Randomize the GOIS scenarios
        int randomScenarioIndex = Random.Range(0, 3); // Selects an index between 0 and 2
        SetScenario(randomScenarioIndex);

        // Store the scenario index in YabberData
        yabberdata.SetGOISScenarioIndex(randomScenarioIndex);
        Debug.Log(randomScenarioIndex);
    }

    // Method to set the current scenario
    public void SetScenario(int scenarioIndex)
    {
        if (scenarioIndex < scenarios.Length)
        {
            currentScenario = scenarios[scenarioIndex];

            // Clear previous button listeners
            foreach (var segment in currentScenario.dialogueSegments)
            {
                segment.triggerButton.onClick.RemoveAllListeners();
            }

            // Assign listeners to each segment's button for the selected scenario
            for (int i = 0; i < currentScenario.dialogueSegments.Length; i++)
            {
                int index = i;  // Capture the index for the lambda expression
                currentScenario.dialogueSegments[i].triggerButton.onClick.AddListener(() => StartDialogueSegment(index));
            }
        }
    }

    void StartDialogueSegment(int segmentIndex)
    {
        if (currentScenario != null && segmentIndex < currentScenario.dialogueSegments.Length)
        {
            StartCoroutine(PlayDialogueSegment(segmentIndex));
        }
    }

    IEnumerator PlayDialogueSegment(int segmentIndex)
    {
        scamText.gameObject.SetActive(true);

        string[] lines = currentScenario.dialogueSegments[segmentIndex].lines;
        AudioClip[] clips = currentScenario.dialogueSegments[segmentIndex].audioClips;
        float[] durations = currentScenario.dialogueSegments[segmentIndex].lineDurations;

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
        foreach (GameObject obj in currentScenario.dialogueSegments[segmentIndex].objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        // Deactivate the specified game objects
        foreach (GameObject obj in currentScenario.dialogueSegments[segmentIndex].objectsToDeactivate)
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
        if (currentScenario != null)
        {
            currentScenario.scamCall.SetActive(false);

            if (currentScenario.declineNotification != null)
            {
                currentScenario.declineNotification.SetActive(true);  // Activate the decline notification
                currentScenario.YabberPing.SetActive(true);
                yabberdata.isGOISClicked = true;
            }
        }
    }
    public void EndCall()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void Lose()
    {
        if (currentScenario != null)
        {
            // Ensure only the correct lose screen is activated
            currentScenario.loseScreen.SetActive(false);  // Deactivate the scam lose screen if necessary
            currentScenario.loseScreen1.SetActive(false); // Deactivate the real scenario lose screen if necessary

            // Determine the scenario index
            int scenarioIndex = System.Array.IndexOf(scenarios, currentScenario);

            if (scenarioIndex == 0 || scenarioIndex == 1)
            {
                currentScenario.loseScreen.SetActive(true);  // Trigger lose for scam scenarios
            }
            else if (scenarioIndex == 2)
            {
                currentScenario.loseScreen1.SetActive(true); // Trigger lose for the real scenario
            }
        }
    }

    public void Win()
    {
        if (currentScenario != null)
        {
            // Ensure only the correct win screen is activated
            currentScenario.winScreen.SetActive(false);  // Deactivate the scam win screen if necessary
            currentScenario.winScreen1.SetActive(false); // Deactivate the real scenario win screen if necessary

            // Determine the scenario index
            int scenarioIndex = System.Array.IndexOf(scenarios, currentScenario);

            if (scenarioIndex == 0 || scenarioIndex == 1)
            {
                currentScenario.winScreen.SetActive(true);  // Trigger win for scam scenarios
            }
            else if (scenarioIndex == 2)
            {
                currentScenario.winScreen1.SetActive(true); // Trigger win for the real scenario
            }
        }
    }


    public void EndCallBtn()
    {
        if (currentScenario != null)
        {
            // Determine the scenario index
            int scenarioIndex = System.Array.IndexOf(scenarios, currentScenario);

            // Check the scenario index and trigger the appropriate screen
            if (scenarioIndex == 0 || scenarioIndex == 1)
            {
                // Trigger win for scam scenarios
                Win();
            }
            else if (scenarioIndex == 2)
            {
                // Trigger lose for the real scenario
                Lose();
            }
        }
    }

    public void INVSclicked()
    {
        yabberdata.isINVSClicked = true;
    }
}
