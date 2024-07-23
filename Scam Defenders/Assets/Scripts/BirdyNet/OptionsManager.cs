using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public bool Scenario1 = false;
    public bool Scenario1n1 = false;

    public bool Scenario2 = false;

    public bool Scenario3 = false;

    public void scenario1()
    {
        Scenario1 = true;
        dialogueManager.optionList1.SetActive(false);
    }
    public void scenario1n1()
    {
        Scenario1n1 = true;
        dialogueManager.optionList2.SetActive(false);
    }
}