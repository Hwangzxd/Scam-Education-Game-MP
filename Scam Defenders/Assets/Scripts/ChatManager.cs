using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ChatManager : MonoBehaviour
{
    public ChatScenario currentScenario;
    public Transform chatContent; // The parent object for chat messages
    public Transform responseContent; // The parent object for response buttons
    public GameObject scammerMessagePrefab; // Prefab for scammer chat messages
    public GameObject playerMessagePrefab; // Prefab for player chat messages
    public GameObject responseButtonPrefab; // Prefab for response buttons
    public GameObject typingAnimationPrefab; // Prefab for typing animation
    public ScrollRect scrollRect; // Reference to the ScrollRect component

    public GameObject winningScreen; // Reference to the winning screen
    public GameObject losingScreen; // Reference to the losing screen

    private int currentMessageIndex = 0;
    private List<GameObject> activeResponseButtons = new List<GameObject>();

    void Start()
    {
        DisplayMessage(currentMessageIndex);
    }

    void DisplayMessage(int messageIndex)
    {
        if (messageIndex >= currentScenario.messages.Count)
        {
            // End of scenario
            return;
        }

        ChatMessage message = currentScenario.messages[messageIndex];
        GameObject messageGO = Instantiate(scammerMessagePrefab, chatContent);
        TMP_Text messageText = messageGO.GetComponentInChildren<TMP_Text>();
        messageText.text = message.messageText;

        // LeanTween animation
        messageGO.transform.localScale = Vector3.zero;
        LeanTween.scale(messageGO, Vector3.one, 0.5f).setEaseOutBounce();

        // Force layout rebuild
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());

        // Scroll to bottom
        StartCoroutine(ScrollToBottom());

        foreach (ChatResponse response in message.responses)
        {
            GameObject responseGO = Instantiate(responseButtonPrefab, responseContent);
            TMP_Text responseText = responseGO.GetComponentInChildren<TMP_Text>();
            responseText.text = response.responseText;

            Button responseButton = responseGO.GetComponentInChildren<Button>();
            responseButton.onClick.AddListener(() => OnResponseSelected(response));
            activeResponseButtons.Add(responseGO); // Track the active response button
        }

        currentMessageIndex++;
    }

    void OnResponseSelected(ChatResponse response)
    {
        // Clear old responses
        foreach (GameObject button in activeResponseButtons)
        {
            Destroy(button);
        }
        activeResponseButtons.Clear();

        // Add player message only if it's not a final choice
        if (!response.isCorrect && !response.isWrong)
        {
            // Add player message
            GameObject playerMessageGO = Instantiate(playerMessagePrefab, chatContent);
            TMP_Text playerMessageText = playerMessageGO.GetComponentInChildren<TMP_Text>();
            playerMessageText.text = response.responseText;

            // Force layout rebuild
            LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());

            // LeanTween animation
            playerMessageGO.transform.localScale = Vector3.zero;
            LeanTween.scale(playerMessageGO, Vector3.one, 0.5f).setEaseOutBounce();

            // Force layout rebuild
            LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());

            // Scroll to bottom
            StartCoroutine(ScrollToBottom());
        }

        if (response.isCorrect)
        {
            // Display winning screen
            Debug.Log("Correct choice");
            winningScreen.SetActive(true);
        }
        else if (response.isWrong)
        {
            // Display losing screen
            Debug.Log("Wrong choice");
            losingScreen.SetActive(true);
        }
        else
        {
            // Start coroutine to display the NPC message after a delay
            if (currentMessageIndex > 0) // Skip typing animation for the first message
            {
                StartCoroutine(DisplayTypingAnimationThenMessage(response.nextMessageIndex, 1.5f)); // 1.5 seconds delay
            }
            else
            {
                StartCoroutine(DisplayNextMessageWithDelay(response.nextMessageIndex, 1.5f)); // 1.5 seconds delay
            }
        }        
    }

    IEnumerator DisplayNextMessageWithDelay(int nextMessageIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        DisplayMessage(nextMessageIndex);
    }

    IEnumerator DisplayTypingAnimationThenMessage(int nextMessageIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Instantiate typing animation
        GameObject typingAnimationGO = Instantiate(typingAnimationPrefab, chatContent);

        // Force layout rebuild
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());

        // Scroll to bottom
        StartCoroutine(ScrollToBottom());

        yield return new WaitForSeconds(delay);

        // Destroy typing animation
        Destroy(typingAnimationGO);

        // Display the next message
        DisplayMessage(nextMessageIndex);
    }

    IEnumerator ScrollToBottom()
    {
        // Wait for the end of the frame to ensure the UI has updated
        yield return new WaitForEndOfFrame();

        // Force the canvas to update to ensure layout calculations are done
        Canvas.ForceUpdateCanvases();

        // Scroll to the bottom
        scrollRect.verticalNormalizedPosition = 0f;

        // Force the canvas to update again to ensure the scroll position is applied
        Canvas.ForceUpdateCanvases();
    }
}