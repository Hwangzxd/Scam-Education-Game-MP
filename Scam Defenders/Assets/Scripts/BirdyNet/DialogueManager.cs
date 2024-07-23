using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public OptionsManager optionsManager;

    public RectTransform messageContainer; // The container for message images
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    public float scaleAnimationDuration = 0.5f; // Duration for the scale animation
    public Vector3 initialScale = Vector3.one * 0.1f; // Initial scale for the size-in animation

    public GameObject optionsBlur; // Blur when no options available
    public GameObject optionList1;
    public GameObject optionList2;
    public GameObject optionList3;

    public GameObject optionListEnd1;
    public GameObject optionListEnd2n1;
    public GameObject optionListEnd2n2;

    // Lose screens
    public GameObject loseScreenNumber;
    public GameObject loseScreenPersuade;
    public GameObject loseScreenMeeting;

    // Win screens
    public GameObject winScreenPersuade;
    public GameObject winScreenMeeting;

    // Arrays to store the pre-existing message GameObjects for each scenario
    public GameObject[] originalMessages;
    public GameObject[] scenario1Messages;
    public GameObject[] scenario2Messages;
    public GameObject[] scenario3Messages;

    private Dictionary<string, IEnumerator> scenarios;

    private void Start()
    {
        // Hide all options initially
        optionList1.SetActive(false);
        optionList2.SetActive(false);
        optionListEnd1.SetActive(false);

        // Enable blur
        optionsBlur.SetActive(true);

        loseScreenNumber.SetActive(false); // Hide the lose screen initially

        // Initialize the scenario dictionary
        scenarios = new Dictionary<string, IEnumerator>
        {
            { "StartScene", StartScene() },
            { "Scenario1", Scenario1() },
            { "Scenario1n1", Scenario1n1() },
            { "Scenario1n2", Scenario1n2() },
            { "Scenario1end1", Scenario1end1() },
            { "Scenario1end2", Scenario1end2() },

            { "Scenario2", Scenario2() },
            { "Scenario2n1", Scenario2n1() },
            { "Scenario2end1", Scenario2end1() },
            { "Scenario2end2", Scenario2end2() },
        };

        // Start the first scenario
        StartCoroutine(scenarios["StartScene"]);
    }

    private void Update()
    {
        // Check which part of scenario 1 is active and run the corresponding coroutine
        if (optionsManager.GetScenarioStatus("Scenario1"))
        {
            StartCoroutine(scenarios["Scenario1"]);
            optionsManager.SetScenarioStatus("Scenario1", false);
        }
        else if (optionsManager.GetScenarioStatus("Scenario1n1"))
        {
            StartCoroutine(scenarios["Scenario1n1"]);
            optionsManager.SetScenarioStatus("Scenario1n1", false);
        }
        else if (optionsManager.GetScenarioStatus("Scenario1n2"))
        {
            StartCoroutine(scenarios["Scenario1n2"]);
            optionsManager.SetScenarioStatus("Scenario1n2", false);
        }
        else if (optionsManager.GetScenarioStatus("Scenario1end1"))
        {
            StartCoroutine(scenarios["Scenario1end1"]);
            optionsManager.SetScenarioStatus("Scenario1end1", false);
        }
        else if (optionsManager.GetScenarioStatus("Scenario1end2"))
        {
            StartCoroutine(scenarios["Scenario1end2"]);
            optionsManager.SetScenarioStatus("Scenario1end2", false);
        }

        // Check which part of scenario 2 is active and run the corresponding coroutine
        if (optionsManager.GetScenarioStatus("Scenario2"))
        {
            StartCoroutine(scenarios["Scenario2"]);
            optionsManager.SetScenarioStatus("Scenario2", false);
        }
        else if (optionsManager.GetScenarioStatus("Scenario2n1"))
        {
            StartCoroutine(scenarios["Scenario2n1"]);
            optionsManager.SetScenarioStatus("Scenario2n1", false);
        }
        else if (optionsManager.GetScenarioStatus("Scenario2end1"))
        {
            StartCoroutine(scenarios["Scenario2end1"]);
            optionsManager.SetScenarioStatus("Scenario2end1", false);
        }
        else if (optionsManager.GetScenarioStatus("Scenario2end2"))
        {
            StartCoroutine(scenarios["Scenario2end2"]);
            optionsManager.SetScenarioStatus("Scenario2end2", false);
        }
    }

    #region Chat Logic

    private IEnumerator StartScene()
    {
        yield return StartCoroutine(ShowMessage(originalMessages[0])); // Jennifer's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
        yield return StartCoroutine(ShowMessage(originalMessages[1])); // User's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the options
        optionsBlur.SetActive(false);
        optionList1.SetActive(true);
    }

    private IEnumerator ShowMessage(GameObject obj)
    {
        // Set the initial scale for the animation
        obj.SetActive(true);
        obj.transform.localScale = initialScale;

        // Ensure the scroll rect is updated to the bottom
        yield return StartCoroutine(UpdateScrollRect());

        // Animate scaling from initialScale to 1
        LeanTween.scale(obj, Vector3.one, scaleAnimationDuration)
            .setEase(LeanTweenType.easeOutBounce);

        // Wait until scaling animation is done
        yield return new WaitForSeconds(scaleAnimationDuration);
    }

    private IEnumerator UpdateScrollRect()
    {
        // Wait for the end of the frame to ensure the layout group updates
        yield return new WaitForEndOfFrame();

        // Force an update on the layout and content size fitter
        LayoutRebuilder.ForceRebuildLayoutImmediate(messageContainer);

        // Scroll to the bottom
        scrollRect.verticalNormalizedPosition = 0;
    }

    #endregion

    #region Scenario 1

    private IEnumerator Scenario1()
    {
        yield return StartCoroutine(ShowMessage(scenario1Messages[0])); // User's response
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShowMessage(scenario1Messages[1])); // Jennifer's response
        yield return new WaitForSeconds(1f);
        optionsBlur.SetActive(false);
        optionList2.SetActive(true);
    }

    private IEnumerator Scenario1n1()
    {
        yield return StartCoroutine(ShowMessage(scenario1Messages[2])); // User's response
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShowMessage(scenario1Messages[3])); // Jennifer's response
        yield return new WaitForSeconds(1f);

        // Wait for a few seconds before showing the lose screen
        yield return new WaitForSeconds(1.5f);

        // Show the lose screen
        loseScreenNumber.SetActive(true);
    }

    private IEnumerator Scenario1n2()
    {
        yield return StartCoroutine(ShowMessage(scenario1Messages[4])); // User's response
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShowMessage(scenario1Messages[5])); // Jennifer's response
        yield return new WaitForSeconds(1f);
        optionsBlur.SetActive(false);
        optionListEnd1.SetActive(true);
    }

    private IEnumerator Scenario1end1()
    {
        // Wait for a few seconds before showing the lose screen
        yield return new WaitForSeconds(1.5f); // Adjust the delay as needed

        // Show the lose screen
        loseScreenPersuade.SetActive(true);
    }

    private IEnumerator Scenario1end2()
    {
        // Wait for a few seconds before showing the win screen
        yield return new WaitForSeconds(1.5f); // Adjust the delay as needed

        // Show the win screen
        winScreenPersuade.SetActive(true);
    }

    #endregion

    #region Scenario 2
    private IEnumerator Scenario2()
    {
        yield return StartCoroutine(ShowMessage(scenario2Messages[0])); // User's response
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShowMessage(scenario2Messages[1])); // Jennifer's response
        yield return new WaitForSeconds(1f);
        optionsBlur.SetActive(false);
        optionList3.SetActive(true);
    }

    private IEnumerator Scenario2n1()
    {
        yield return StartCoroutine(ShowMessage(scenario2Messages[2])); // User's response
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShowMessage(scenario2Messages[3])); // Jennifer's response
        yield return new WaitForSeconds(1f);
        optionsBlur.SetActive(false);
        optionListEnd2n1.SetActive(true);
    }

    private IEnumerator Scenario2n2()
    {
        yield return StartCoroutine(ShowMessage(scenario2Messages[2])); // User's response
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShowMessage(scenario2Messages[3])); // Jennifer's response
        yield return new WaitForSeconds(1f);
        optionsBlur.SetActive(false);
        optionListEnd2n2.SetActive(true);
    }

    private IEnumerator Scenario2end1()
    {
        // Wait for a few seconds before showing the lose screen
        yield return new WaitForSeconds(1.5f); // Adjust the delay as needed

        // Show the lose screen
        loseScreenMeeting.SetActive(true);
    }

    private IEnumerator Scenario2end2()
    {
        // Wait for a few seconds before showing the win screen
        yield return new WaitForSeconds(1.5f); // Adjust the delay as needed

        // Show the win screen
        winScreenMeeting.SetActive(true);
    }

    #endregion
}
