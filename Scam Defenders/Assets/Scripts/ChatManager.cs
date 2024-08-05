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
    public ScrollRect scrollRect; // Reference to the ScrollRect component

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

        // Add player message
        GameObject playerMessageGO = Instantiate(playerMessagePrefab, chatContent);
        TMP_Text playerMessageText = playerMessageGO.GetComponentInChildren<TMP_Text>();
        playerMessageText.text = response.responseText;

        // Scroll to bottom
        StartCoroutine(ScrollToBottom());

        //DisplayMessage(response.nextMessageIndex);

        // Start coroutine to display the NPC message after a delay
        StartCoroutine(DisplayNextMessageWithDelay(response.nextMessageIndex, 1.5f)); // 1.5 seconds delay
    }

    IEnumerator DisplayNextMessageWithDelay(int nextMessageIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
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