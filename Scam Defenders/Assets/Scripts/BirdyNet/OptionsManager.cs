using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public DialogueManager dialogueManager;

    private Dictionary<string, bool> scenarioStates;

    private void Start()
    {
        // Initialize scenario states
        scenarioStates = new Dictionary<string, bool>
        {
            { "Scenario1", false },
            { "Scenario1n1", false },
            { "Scenario1n2", false },
            { "Scenario1end1", false },
            { "Scenario1end2", false },

            { "Scenario2", false },
            { "Scenario2n1", false },
            { "Scenario2n2", false },
            { "Scenario2end1", false },
            { "Scenario2end2", false },

            { "Scenario3", false }
        };
    }

    public bool GetScenarioStatus(string scenarioKey)
    {
        return scenarioStates.ContainsKey(scenarioKey) && scenarioStates[scenarioKey];
    }

    public void SetScenarioStatus(string scenarioKey, bool status)
    {
        if (scenarioStates.ContainsKey(scenarioKey))
        {
            scenarioStates[scenarioKey] = status;
        }
    }

    #region Scenario 1

    public void Scenario1()
    {
        SetScenarioStatus("Scenario1", true);
        dialogueManager.optionList1.SetActive(false);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario1n1()
    {
        SetScenarioStatus("Scenario1n1", true);
        dialogueManager.optionList2.SetActive(false);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario1n2()
    {
        SetScenarioStatus("Scenario1n2", true);
        dialogueManager.optionList2.SetActive(false);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario1nend1()
    {
        SetScenarioStatus("Scenario1end1", true);
        dialogueManager.optionList2.SetActive(false);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario1nend2()
    {
        SetScenarioStatus("Scenario1end2", true);
        dialogueManager.optionList2.SetActive(false);
        dialogueManager.optionsBlur.SetActive(true);
    }

    #endregion

    #region Scenario 2
    public void Scenario2()
    {
        SetScenarioStatus("Scenario2", true);
        dialogueManager.optionList1.SetActive(false);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario2n1()
    {
        SetScenarioStatus("Scenario2n1", true);
        dialogueManager.optionList3.SetActive(false);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario2nend1()
    {
        SetScenarioStatus("Scenario2end1", true);
        dialogueManager.optionList3.SetActive(false);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario2nend2()
    {
        SetScenarioStatus("Scenario2end2", true);
        dialogueManager.optionList3.SetActive(false);
        dialogueManager.optionsBlur.SetActive(true);
    }

    #endregion

    // Pop-up buttons
    public void Continue()
    {
        // Implement continue button functionality
    }

    // Option buttons for other scenarios (if any)
}
