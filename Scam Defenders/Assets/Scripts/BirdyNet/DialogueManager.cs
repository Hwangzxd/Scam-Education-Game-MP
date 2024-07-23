using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public OptionsManager optionsManager;

    public RectTransform messageContainer; // The container for message images
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    public float scaleAnimationDuration = 0.5f; // Duration for the scale animation
    public Vector3 initialScale = Vector3.one * 0.1f; // Initial scale for the size-in animation

    public GameObject optionList1; // option buttons
    public GameObject optionList2; // option buttons

    // Arrays to store the pre-existing message GameObjects for each scenario
    public GameObject[] originalMessages;

    public GameObject[] scenario1Messages;
    public GameObject[] scenario2Messages;
    public GameObject[] scenario3Messages;

    private void Start()
    {
        // Hide all options initially
        optionList1.SetActive(false);
        optionList2.SetActive(false);

        // Start the first scenario
        StartCoroutine(StartScene());
    }

    private void Update()
    {
        if (optionsManager.Scenario1)
        {
            StartCoroutine(Scenario1());
            optionsManager.Scenario1 = false;
        }
        if (optionsManager.Scenario2)
        {

        }
        if (optionsManager.Scenario3)
        {

        }

    }

    private IEnumerator StartScene()
    {
        yield return StartCoroutine(ShowMessage(originalMessages[0])); // Jennifer's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the next message
        yield return StartCoroutine(ShowMessage(originalMessages[1])); // User's first message
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing the options
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

    private IEnumerator Scenario1()
    {
        yield return StartCoroutine(ShowMessage(scenario1Messages[0])); // User's response
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShowMessage(scenario1Messages[1])); // Jennifer's response
        yield return new WaitForSeconds(1f);
        optionList2.SetActive(true);
    }
}