using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chat Scenario", menuName = "Chat Scenario")]
public class ChatScenario : ScriptableObject
{
    public string scenarioName;
    public List<ChatMessage> messages;
}

[System.Serializable]
public class ChatMessage
{
    //public string messageText;
    public List<string> messageTexts; // List of messages to be sent in a row
    public List<ChatResponse> responses; // Only used for NPC messages
}

[System.Serializable]
public class ChatResponse
{
    //public string responseText;
    public List<string> responseTexts; // List of player response messages
    public bool winScreen1;
    public bool winScreen2;
    public bool winScreen3;
    public bool winScreen4;
    public bool loseScreen1;
    public bool loseScreen2;
    public bool loseScreen3;
    public bool loseScreen4;
    public int nextMessageIndex; // Index of the next message in the scenario
}