using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManagerGois : MonoBehaviour
{
    public OptionsManagerGois OptionsManagerGois;

    public RectTransform messageContainer; // The container for message images
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    public float scaleAnimationDuration = 0.5f; // Duration for the scale animation
    public Vector3 initialScale = Vector3.one * 0.1f; // Initial scale for the size-in animation


    // Lose screens
    public GameObject loseScreen1;
    //etc.

    // Win screens
    public GameObject winScreen1;
    //etc.

    // Arrays to store the pre-existing message GameObjects for each scenario
    public GameObject[] originalMessages;
    public GameObject[] scenario3Messages;
    public GameObject[] scenario4Messages;

    //others
    public GameObject CallUI;
    public TextMeshProUGUI CallText;
    public float fadeDuration;

    private Dictionary<string, IEnumerator> scenarios;

    private void Start()
    {
        // Initialize the scenario dictionary
        scenarios = new Dictionary<string, IEnumerator>
        {
            { "StartScene", StartScene() },
        };

        // Start the original message
        StartCoroutine(scenarios["StartScene"]);
    }


    #region Original Message
    private IEnumerator StartScene()
    {
        yield return StartCoroutine(ShowMessage(originalMessages[0])); // Scammer's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
    }

    #endregion

    #region Scenario 1

    public IEnumerator Scenario1()
    {
        //fade in CallUI
        CanvasGroup callCanvasGroup = CallUI.GetComponent<CanvasGroup>();
        if (callCanvasGroup == null)
        {
            callCanvasGroup = CallUI.AddComponent<CanvasGroup>();
        }
        callCanvasGroup.alpha = 0;
        CallUI.SetActive(true);
        LeanTween.alphaCanvas(callCanvasGroup, 1, fadeDuration);

        yield return new WaitForSeconds(3f); //wait for 3 seconds

        CallText.text = "Is there an Officer Tan for tax filing?";
        yield return new WaitForSeconds(2f); //wait for 2 seconds

        CallText.text = "No.";
        CallText.color = Color.green; //change text color to green
        yield return new WaitForSeconds(2f); //wait for 2 seconds

        //fade out CallUI
        LeanTween.alphaCanvas(callCanvasGroup, 0, fadeDuration).setOnComplete(() => CallUI.SetActive(false));

        CallText.color = Color.white; //set text color to white
        CallText.text = "Ringing...";
    }


    #endregion

    #region Scenario 2
    public IEnumerator Scenario2()
    {
        yield return StartCoroutine(ShowMessage(scenario3Messages[0])); // User's message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
        yield return StartCoroutine(ShowMessage(scenario4Messages[1])); // Scammer's message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
    }

    #endregion

    #region Scenario 3
    public IEnumerator Scenario3()
    {
        yield return StartCoroutine(ShowMessage(scenario3Messages[0])); // User's message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
        yield return StartCoroutine(ShowMessage(scenario4Messages[1])); // Scammer's message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
    }

    #endregion

    #region Scenario 4
    public IEnumerator Scenario4()
    {
        yield return StartCoroutine(ShowMessage(scenario4Messages[0])); // User's message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
        yield return StartCoroutine(ShowMessage(scenario4Messages[1])); // Scammer's message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
    }

    #endregion


    #region Chat Logic

    //show msg + anims
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

    //helper methods
    public void HideAllOriginalMessages()
    {
        foreach (GameObject message in originalMessages)
        {
            message.SetActive(false);
        }
    }

    public void ShowAllOriginalMessages()
    {
        foreach (GameObject message in originalMessages)
        {
            message.SetActive(true);
        }
    }

    //win-lose screens
    public IEnumerator lose1()
    {
        // Wait for a few seconds before showing the lose screen
        yield return new WaitForSeconds(1f); // Adjust the delay as needed

        // Show the lose screen
        loseScreen1.SetActive(true);
    }

    public IEnumerator win1()
    {
        // Wait for a few seconds before showing the win screen
        yield return new WaitForSeconds(1f); // Adjust the delay as needed

        // Show the win screen
        winScreen1.SetActive(true);
    }

    #endregion
}