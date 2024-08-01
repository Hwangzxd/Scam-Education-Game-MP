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
    public string messageText;
    public bool isPlayerMessage; // To distinguish between player and NPC messages
    public List<ChatResponse> responses; // Only used for NPC messages
}

[System.Serializable]
public class ChatResponse
{
    public string responseText;
    public bool isCorrect;
    public int nextMessageIndex; // Index of the next message in the scenario
}
