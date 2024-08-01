using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public ChatScenario currentScenario;
    public Transform chatContent; // The parent object for chat messages
    public Transform responseContent; // The parent object for response buttons
    public GameObject messagePrefab; // Prefab for chat messages
    public GameObject responseButtonPrefab; // Prefab for response buttons

    private int currentMessageIndex = 0;

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
        GameObject messageGO = Instantiate(messagePrefab, chatContent);
        TMP_Text messageText = messageGO.GetComponentInChildren<TMP_Text>();
        messageText.text = message.messageText;

        if (!message.isPlayerMessage)
        {
            foreach (ChatResponse response in message.responses)
            {
                GameObject responseGO = Instantiate(responseButtonPrefab, responseContent);
                TMP_Text responseText = responseGO.GetComponentInChildren<TMP_Text>();
                responseText.text = response.responseText;

                Button responseButton = responseGO.GetComponentInChildren<Button>();
                responseButton.onClick.AddListener(() => OnResponseSelected(response));
            }
        }

        currentMessageIndex++;
    }

    void OnResponseSelected(ChatResponse response)
    {
        // Clear old responses
        foreach (Transform child in chatContent)
        {
            if (child.GetComponent<Button>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        DisplayMessage(response.nextMessageIndex);
    }
}
