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
            { "Scenario2nend1", false },
            { "Scenario2nend2", false },

            { "Scenario3", false },
            { "Scenario3n1", false },
            { "Scenario3n2", false },
            { "Scenario3end1", false },
            { "Scenario3end2", false },
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

    private void CloseAllOptionLists()
    {
        dialogueManager.optionList1.SetActive(false);
        dialogueManager.optionList2.SetActive(false);
        dialogueManager.optionList3.SetActive(false);
        dialogueManager.optionList4.SetActive(false);
        dialogueManager.optionListEnd1.SetActive(false);
        dialogueManager.optionListEnd2n1.SetActive(false);
        dialogueManager.optionListEnd2n2.SetActive(false);
        dialogueManager.optionListEnd3.SetActive(false);
    }

    #region Scenario 1

    public void Scenario1()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario1", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario1n1()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario1n1", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario1n2()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario1n2", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario1end1()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario1end1", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario1end2()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario1end2", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    #endregion

    #region Scenario 2

    public void Scenario2()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario2", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario2n1()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario2n1", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario2n2()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario2n2", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario2end1()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario2end1", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario2end2()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario2end2", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario2nend1()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario2nend1", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario2nend2()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario2nend2", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    #endregion

    #region Scenario 3

    public void Scenario3()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario3", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario3n1()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario3n1", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario3n2()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario3n2", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario3end1()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario3end1", true);
        dialogueManager.optionsBlur.SetActive(true);
    }

    public void Scenario3end2()
    {
        CloseAllOptionLists();
        SetScenarioStatus("Scenario3end2", true);
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
