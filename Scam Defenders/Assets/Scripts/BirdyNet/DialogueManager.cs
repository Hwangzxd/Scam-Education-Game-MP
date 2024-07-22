using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public RectTransform messageContainer; // The container for message images
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    public float scaleAnimationDuration = 0.5f; // Duration for the scale animation
    public Vector3 initialScale = Vector3.one * 0.1f; // Initial scale for the size-in animation

    public GameObject optionList1; // Array of option buttons

    // Arrays to store the pre-existing message GameObjects for each scenario
    public GameObject[] scenario1Messages;

    private int currentScenario = 0;
    private int currentMessageIndex = 0;

    private void Start()
    {
        // Hide all options initially
        optionList1.SetActive(false);

        // Start the first scenario
        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        yield return StartCoroutine(ShowMessage(scenario1Messages[0])); // Jennifer's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
        yield return StartCoroutine(ShowMessage(scenario1Messages[1])); // User's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the options
        optionList1.SetActive(true);
    }

    private IEnumerator ShowMessage(GameObject messageObject)
    {
        // Store the current scale
        Vector3 currentScale = messageObject.transform.localScale;

        // Set the initial scale for the animation
        messageObject.SetActive(true);
        messageObject.transform.localScale = initialScale;

        // Animate scaling from initialScale to the current scale
        LeanTween.scale(messageObject, currentScale, scaleAnimationDuration)
            .setEase(LeanTweenType.easeOutBounce);

        // Wait until scaling animation is done
        yield return new WaitForSeconds(scaleAnimationDuration);
    }

    private IEnumerator ContinueScenario1()
    {
        yield return StartCoroutine(ShowMessage(scenario1Messages[2])); // User's response
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShowMessage(scenario1Messages[3])); // Jennifer's response
        yield return new WaitForSeconds(1f);
    }
}
