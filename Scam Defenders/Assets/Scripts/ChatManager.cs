using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public ChatScenario currentScenario;
    public Transform chatContent; // The parent object for chat messages
    public Transform responseContent; // The parent object for response buttons
    public GameObject scammerMessagePrefab; // Prefab for scammer chat messages
    public GameObject playerMessagePrefab; // Prefab for player chat messages
    public GameObject responseButtonPrefab; // Prefab for response buttons

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

        DisplayMessage(response.nextMessageIndex);
    }
}