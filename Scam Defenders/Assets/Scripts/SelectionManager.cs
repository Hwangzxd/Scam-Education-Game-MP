using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInput;

    [HideInInspector] public string playerNameKey = "playerName";
    string playerName;

    bool namesCreated = false;

    [SerializeField] Button confirmButton;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //if player's name input is empty, set player's name to "Player"(Default)
        //playerName = "Player";
        Debug.Log("Name set");
        SetPlayerName(playerNameKey, playerName);

        playerNameInput.characterLimit = 12;
    }
    public void SaveName()
    {
        if (playerNameInput.text.Replace(" ", "") != "")
        {
            playerName = playerNameInput.text;
            SetPlayerName(playerNameKey, playerName);
            namesCreated = true;
        }
        else
        {
            namesCreated = false;
        }

        CheckNameInput();
    }

    public void SetPlayerName(string playerNameKey, string playerName)
    {
        PlayerPrefs.SetString(playerNameKey, playerName);
    }

    public string GetPlayerName()
    {
        return PlayerPrefs.GetString(playerNameKey);
    }

    public void CheckNameInput()
    {
        if (namesCreated)
        {
            confirmButton.enabled = true;
            confirmButton.GetComponent<Image>().color = Color.white;

        }
    }

}
