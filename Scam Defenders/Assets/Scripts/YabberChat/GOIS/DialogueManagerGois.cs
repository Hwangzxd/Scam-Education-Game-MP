using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerGois : MonoBehaviour
{
    public OptionsManagerGois OptionsManagerGois;

    public RectTransform messageContainer; // The container for message images
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    public float scaleAnimationDuration = 0.5f; // Duration for the scale animation
    public Vector3 initialScale = Vector3.one * 0.1f; // Initial scale for the size-in animation
    public GameObject typingAnimationPrefab;

    // Lose screens
    public GameObject loseScreen;
    //etc.

    // Win screens
    public GameObject winScreen;
    //etc.

    // Arrays to store the pre-existing message GameObjects for each scenario
    public GameObject[] originalMessages;

    //others
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
        OptionsManagerGois.disableAllButtons();

        yield return StartCoroutine(DisplayTypingAnimation(2f));
        yield return StartCoroutine(ShowMessage(originalMessages[0])); // Scammer's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message

        OptionsManagerGois.enableAllButtons();
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
    private IEnumerator ShowMessagePrefab(GameObject prefab, Transform parent)
    {
        // Instantiate the prefab
        GameObject obj = Instantiate(prefab, parent);

        // Set the initial scale for the animation
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
    public IEnumerator lose()
    {
        // Wait for a few seconds before showing the lose screen
        yield return new WaitForSeconds(1f); // Adjust the delay as needed

        // Show the lose screen
        loseScreen.SetActive(true);
    }

    public IEnumerator win()
    {
        // Wait for a few seconds before showing the win screen
        yield return new WaitForSeconds(1f); // Adjust the delay as needed

        // Show the win screen
        winScreen.SetActive(true);
    }

    #endregion
}