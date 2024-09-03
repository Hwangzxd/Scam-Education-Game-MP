using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ChatManager : MonoBehaviour
{
    public ChatScenario currentScenario;
    public BirdyData birdyData;
    public Transform chatContent; // The parent object for chat messages
    public Transform responseContent; // The parent object for response buttons
    public GameObject scammerMessagePrefab; // Prefab for scammer chat messages
    public GameObject playerMessagePrefab; // Prefab for player chat messages
    public GameObject responseButtonPrefab; // Prefab for response buttons
    public GameObject typingAnimationPrefab; // Prefab for typing animation
    public ScrollRect scrollRect; // Reference to the ScrollRect component

    public Image senderImage;            // Reference to the Image component for the sender's avatar
    public TextMeshProUGUI senderText;   // Reference to the TextMeshProUGUI component for the sender's name

    public List<GameObject> winScreens;
    public List<GameObject> loseScreens;

    private int currentMessageIndex = 0;
    private List<GameObject> activeResponseButtons = new List<GameObject>();

    void Start()
    {
        SetScenarioAvatarsAndNames(1);
        DisplayMessage(currentMessageIndex);
    }

    //For saved data
    public void SetScenarioAvatarsAndNames(int scenarioNumber)
    {
        string playerGender = PlayerPrefs.GetString("playerGender");
        //Debug.Log(playerGender);
        if (string.IsNullOrEmpty(playerGender))
        {
            Debug.LogWarning("No gender saved in PlayerPrefs");
            return; // Exit the method if no gender is saved
        }

        switch (scenarioNumber)
        {
            case 1:
                if (playerGender == "Male")
                {
                    senderImage.sprite = birdyData.girlAvatar1;
                    senderText.text = birdyData.girlName1;
                }
                else if (playerGender == "Female")
                {
                    senderImage.sprite = birdyData.guyAvatar1;
                    senderText.text = birdyData.guyName1;
                }
                break;

            case 2:
                if (playerGender == "Male")
                {
                    senderImage.sprite = birdyData.girlAvatar2;
                    senderText.text = birdyData.girlName2;
                }
                else if (playerGender == "Female")
                {
                    senderImage.sprite = birdyData.guyAvatar2;
                    senderText.text = birdyData.guyName2;
                }
                break;

            case 3:
                if (playerGender == "Male")
                {
                    senderImage.sprite = birdyData.girlAvatar3;
                    senderText.text = birdyData.girlName3;
                }
                else if (playerGender == "Female")
                {
                    senderImage.sprite = birdyData.guyAvatar3;
                    senderText.text = birdyData.guyName3;
                }
                break;

            default:
                Debug.LogWarning("Invalid scenario number");
                break;
        }
    }

    void DisplayMessage(int messageIndex)
    {
        if (messageIndex >= currentScenario.messages.Count)
        {
            // End of scenario
            return;
        }

        ChatMessage message = currentScenario.messages[messageIndex];

        // Display each message in the messageTexts list
        StartCoroutine(DisplayMessagesInSequence(message.messageTexts, message.responses));
    }

    IEnumerator DisplayMessagesInSequence(List<string> messageTexts, List<ChatResponse> responses)
    {
        foreach (var text in messageTexts)
        {
            GameObject messageGO = Instantiate(scammerMessagePrefab, chatContent);
            TMP_Text messageText = messageGO.GetComponentInChildren<TMP_Text>();
            messageText.text = text;

            // Force layout rebuild
            LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());

            // LeanTween animation
            messageGO.transform.localScale = Vector3.zero;
            LeanTween.scale(messageGO, Vector3.one, 0.5f).setEaseOutBounce();

            // Force layout rebuild
            LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());

            // Scroll to bottom
            StartCoroutine(ScrollToBottom());

            // Add a small delay between displaying messages
            yield return new WaitForSeconds(1.5f); // Adjust timing as needed
        }

        // After all messages are displayed, show the response buttons if available
        foreach (ChatResponse response in responses)
        {
            GameObject responseGO = Instantiate(responseButtonPrefab, responseContent);
            TMP_Text responseText = responseGO.GetComponentInChildren<TMP_Text>();
            responseText.text = response.responseTexts[0];

            Button responseButton = responseGO.GetComponentInChildren<Button>();
            responseButton.onClick.AddListener(() => OnResponseSelected(response));
            activeResponseButtons.Add(responseGO);
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

        // Display win/lose screens if applicable
        if (DisplayEndingScreen(response)) return;

        // Display player messages in sequence
        StartCoroutine(DisplayPlayerMessagesInSequence(response.responseTexts, response.nextMessageIndex));
    }

    bool DisplayEndingScreen(ChatResponse response)
    {
        // Check if the response leads to a win or lose screen
        if (response.winScreen1) winScreens[0].SetActive(true);
        else if (response.winScreen2) winScreens[1].SetActive(true);
        else if (response.winScreen3) winScreens[2].SetActive(true);
        //else if (response.winScreen4) winScreens[3].SetActive(true);
        //else if (response.winScreen5) winScreens[4].SetActive(true);
        //else if (response.winScreen6) winScreens[5].SetActive(true);
        else if (response.loseScreen1) loseScreens[0].SetActive(true);
        else if (response.loseScreen2) loseScreens[1].SetActive(true);
        else if (response.loseScreen3) loseScreens[2].SetActive(true);
        //else if (response.loseScreen4) loseScreens[3].SetActive(true);
        //else if (response.loseScreen5) loseScreens[4].SetActive(true);
        //else if (response.loseScreen6) loseScreens[5].SetActive(true);
        else
            return false; // No ending screen displayed, continue normally

        return true; // An ending screen was displayed
    }

    IEnumerator DisplayPlayerMessagesInSequence(List<string> responseTexts, int nextMessageIndex)
    {
        foreach (var text in responseTexts)
        {
            // Add player message
            GameObject playerMessageGO = Instantiate(playerMessagePrefab, chatContent);
            TMP_Text playerMessageText = playerMessageGO.GetComponentInChildren<TMP_Text>();
            playerMessageText.text = text;

            // Force layout rebuild
            LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());

            // LeanTween animation
            playerMessageGO.transform.localScale = Vector3.zero;
            LeanTween.scale(playerMessageGO, Vector3.one, 0.5f).setEaseOutBounce();

            // Force layout rebuild
            LayoutRebuilder.ForceRebuildLayoutImmediate(chatContent.GetComponent<RectTransform>());

            // Scroll to bottom
            StartCoroutine(ScrollToBottom());

            // Add a small delay between messages
            yield return new WaitForSeconds(1.5f); // Adjust timing as needed
        }

        if (currentMessageIndex > 0) // Skip typing animation for the first message
        {
            StartCoroutine(DisplayTypingAnimationThenMessage(nextMessageIndex, 1.0f));
        }
        else
        {
            StartCoroutine(DisplayNextMessageWithDelay(nextMessageIndex, 0.5f));
        }

        //// After all player messages, move to the next NPC message
        //StartCoroutine(DisplayNextMessageWithDelay(nextMessageIndex, 1.5f));
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