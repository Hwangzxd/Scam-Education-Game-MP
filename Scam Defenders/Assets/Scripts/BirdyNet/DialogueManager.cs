using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public RectTransform messageContainer; // The container for message images
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    public float animationDuration = 0.5f; // Duration for animations

    public GameObject optionList1; // Array of option buttons
    //public GameObject optionList2; // Array of option buttons

    // Arrays to store the pre-existing message GameObjects for each scenario
    public GameObject[] scenario1Messages;
    private int currentScenario = 0;
    private int currentMessageIndex = 0;

    private void Start()
    {
        // Hide all options initially
        optionList1.SetActive(false);
        //optionList2.SetActive(false);

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
        messageObject.SetActive(true);
        yield return StartCoroutine(AnimateScroll(messageObject));
    }


    private IEnumerator AnimateScroll(GameObject messageObject)
    {
        Canvas.ForceUpdateCanvases();
        float elapsedTime = 0f;
        Vector2 startPos = scrollRect.content.anchoredPosition;
        Vector2 endPos = new Vector2(startPos.x, startPos.y + messageObject.GetComponent<RectTransform>().rect.height);

        while (elapsedTime < animationDuration)
        {
            scrollRect.content.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        scrollRect.content.anchoredPosition = endPos;
    }


    private IEnumerator ContinueScenario1()
    {
        yield return StartCoroutine(ShowMessage(scenario1Messages[2])); // User's response
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShowMessage(scenario1Messages[3])); // Jennifer's response
        yield return new WaitForSeconds(1f);

    }
}
