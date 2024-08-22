using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerInvs : MonoBehaviour
{
    public OptionsManagerInvs OptionsManagerInvs;

    public RectTransform messageContainer; // The container for message images
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    public float scaleAnimationDuration = 0.5f; // Duration for the scale animation
    public Vector3 initialScale = Vector3.one * 0.1f; // Initial scale for the size-in animation
    public GameObject typingAnimationPrefab;

    // Lose screens
    public GameObject loseScreen1;
    public GameObject loseScreen2;

    // Win screens
    public GameObject winScreen1;
    public GameObject winScreen2;

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

    private IEnumerator StartScene()
    {
        yield return StartCoroutine(ShowMessage(originalMessages[0])); // Scammer's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
    }

    public IEnumerator RequestOfficialDocumentations()
    {
        if (!OptionsManagerInvs.originalMessage.activeSelf)
        {
            OptionsManagerInvs.originalMessage.SetActive(true);
        }

        yield return StartCoroutine(ShowMessage(originalMessages[1])); // Player's message
        yield return StartCoroutine(DisplayTypingAnimation(3f)); // Wait for 3 seconds before showing the next message
        yield return StartCoroutine(ShowMessage(originalMessages[2])); // Scammer's message
        yield return new WaitForSeconds(1f);
        OptionsManagerInvs.enableAllButtons();
    }

    public IEnumerator ContactIndependentFinancialAdvisor()
    {
        yield return StartCoroutine(ShowMessage(originalMessagesAdvisor[0])); // Player's message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
        yield return StartCoroutine(ShowMessage(originalMessagesAdvisor[1])); // Player's message
        yield return StartCoroutine(DisplayTypingAnimation(3f)); // Wait for 3 seconds before showing the next message
        yield return StartCoroutine(ShowMessage(originalMessagesAdvisor[2])); // Scammer's message
        yield return StartCoroutine(DisplayTypingAnimation(2f)); // Wait for 2 seconds before showing the next message
        yield return StartCoroutine(ShowMessage(originalMessagesAdvisor[3])); // Scammer's message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
        OptionsManagerInvs.enableAllButtons();
    }

    public IEnumerator lose1()
    {
        yield return new WaitForSeconds(1f);
        loseScreen1.SetActive(true);
    }

    public IEnumerator win1()
    {
        yield return new WaitForSeconds(1f);
        winScreen1.SetActive(true);
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
        LeanTween.scale(obj, Vector3.one, scaleAnimationDuration).setEase(LeanTweenType.easeOutBounce);

        // Wait until scaling animation is done
        yield return new WaitForSeconds(scaleAnimationDuration);
    }

    IEnumerator DisplayTypingAnimation(float duration)
    {
        // Check if the typing animation is already instantiated
        Transform existingTypingAnimation = messageContainer.Find(typingAnimationPrefab.name);

        if (existingTypingAnimation != null)
        {
            // Typing animation already exists, so just wait for the specified duration
            yield return new WaitForSeconds(duration);
        }
        else
        {
            // Instantiate typing animation
            GameObject typingAnimationGO = Instantiate(typingAnimationPrefab, messageContainer);
            typingAnimationGO.name = typingAnimationPrefab.name; // Assign a unique name for easy identification

            // Force layout rebuild
            LayoutRebuilder.ForceRebuildLayoutImmediate(messageContainer.GetComponent<RectTransform>());

            // Scroll to bottom
            yield return StartCoroutine(UpdateScrollRect());

            // Wait for the specified duration
            yield return new WaitForSeconds(duration);

            // Destroy typing animation
            Destroy(typingAnimationGO);
        }
    }


    private IEnumerator UpdateScrollRect()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(messageContainer);
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
