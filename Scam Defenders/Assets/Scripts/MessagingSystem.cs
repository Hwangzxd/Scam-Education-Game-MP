using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessagingSystem : MonoBehaviour
{
    public GameObject messagePrefab; // Prefab for displaying messages
    public Transform messageContainer; // Container for message prefabs
    public Button[] optionButtons; // Buttons for player responses

    public List<Message> initialMessages; // List of initial messages for easier Inspector editing

    private Queue<Message> messageQueue = new Queue<Message>();

    void Start()
    {
        LoadInitialMessages();
        StartCoroutine(DisplayMessagesWithDelay());
    }

    void LoadInitialMessages()
    {
        foreach (Message message in initialMessages)
        {
            messageQueue.Enqueue(message);
        }
    }

    //public void StartConversation()
    //{
    //    StartCoroutine(DisplayMessagesWithDelay());
    //}

    IEnumerator DisplayMessagesWithDelay()
    {
        while (messageQueue.Count > 0)
        {
            Message currentMessage = messageQueue.Dequeue();
            Debug.Log($"Displaying message: {currentMessage.Text}");

            GameObject newMessage = Instantiate(messagePrefab, messageContainer);
            TMP_Text messageText = newMessage.GetComponentInChildren<TMP_Text>();

            if (messageText == null)
            {
                Debug.LogError("Message prefab does not have a Text component.");
                yield break; // Exit the coroutine if the Text component is missing
            }

            messageText.text = currentMessage.Text;

            for (int i = 0; i < optionButtons.Length; i++)
            {
                if (i < currentMessage.Options.Count)
                {
                    optionButtons[i].gameObject.SetActive(true);
                    optionButtons[i].GetComponentInChildren<TMP_Text>().text = currentMessage.Options[i].Text;
                    int index = i; // Prevent closure issue
                    optionButtons[i].onClick.RemoveAllListeners();
                    optionButtons[i].onClick.AddListener(() => currentMessage.Options[index].Action());
                }
                else
                {
                    optionButtons[i].gameObject.SetActive(false);
                }
            }

            yield return new WaitForSeconds(3f);
        }
    }

    public void NextMessage()
    {
        // Load the next set of messages or actions
        messageQueue.Enqueue(new Message("This is the next message after option!", new List<Option>
        {
            new Option("Continue", NextMessage)
        }));

        StartCoroutine(DisplayMessagesWithDelay());
    }
}

[System.Serializable]
public class Message
{
    public string Text;
    public List<Option> Options;

    public Message(string text, List<Option> options)
    {
        Text = text;
        Options = options;
    }
}

[System.Serializable]
public class Option
{
    public string Text;
    public UnityEngine.Events.UnityAction Action;

    public Option(string text, UnityEngine.Events.UnityAction action)
    {
        Text = text;
        Action = action;
    }
}