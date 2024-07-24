using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerYabber : MonoBehaviour
{
    public OptionsManagerYabber YabberOptionsManager;

    public RectTransform messageContainer; // The container for message images
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    public float scaleAnimationDuration = 0.5f; // Duration for the scale animation
    public Vector3 initialScale = Vector3.one * 0.1f; // Initial scale for the size-in animation


    // Lose screens
    //public GameObject loseScreen1;
    //etc.

    // Win screens
    //public GameObject winScreen1;
    //etc.

    // Arrays to store the pre-existing message GameObjects for each scenario
    public GameObject[] originalMessages;
    public GameObject[] originalMessagesAdvisor;
    public GameObject[] scenario2Messages;
    public GameObject[] scenario3Messages;

    private Dictionary<string, IEnumerator> scenarios;

    private void Start()
    {


        // Initialize the scenario dictionary
        scenarios = new Dictionary<string, IEnumerator>
        {
            { "StartScene", StartScene() },


        };

        // Start the first scenario
        StartCoroutine(scenarios["StartScene"]);
    }

    private void Update()
    {


    }

    private IEnumerator StartScene()
    {
        yield return StartCoroutine(ShowMessage(originalMessages[0])); // Scammer's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message

    }

    public IEnumerator RequestOfficialDocumentations()
    {
        if (!YabberOptionsManager.originalMessage.activeSelf)
        {
            YabberOptionsManager.originalMessage.SetActive(true);
        }
        yield return StartCoroutine(ShowMessage(originalMessages[1])); // Scammer's first message
        yield return new WaitForSeconds(3f); // Wait for 3 second before showing the next message
        yield return StartCoroutine(ShowMessage(originalMessages[2])); // Scammer's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
        YabberOptionsManager.enableAllButtons();

    }

    public IEnumerator ContactIndependentFinancialAdvisor()
    {
        yield return StartCoroutine(ShowMessage(originalMessagesAdvisor[0])); // Scammer's first message
        yield return new WaitForSeconds(1f); // Wait for 3 second before showing the next message
        yield return StartCoroutine(ShowMessage(originalMessagesAdvisor[1])); // Scammer's first message
        yield return new WaitForSeconds(3f); // Wait for 1 second before showing the next message
        yield return StartCoroutine(ShowMessage(originalMessagesAdvisor[2])); // Scammer's first message
        yield return new WaitForSeconds(2f); // Wait for 3 second before showing the next message
        yield return StartCoroutine(ShowMessage(originalMessagesAdvisor[3])); // Scammer's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
        YabberOptionsManager.enableAllButtons();

    }
    #region Chat Logic



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

    public void HideAllAdvisorMessages()
    {
        foreach (GameObject message in originalMessagesAdvisor)
        {
            message.SetActive(false);
        }
    }

    public void ShowAllAdvisorMessages()
    {
        foreach (GameObject message in originalMessagesAdvisor)
        {
            message.SetActive(true);
        }
    }


    #endregion



}
