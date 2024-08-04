using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManagerGois : MonoBehaviour
{
    public DialogueManagerGois DialogueManagerGois;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button backBtn;


    void Start()
    {
        // Assign listeners to the buttons
        button1.onClick.AddListener(OnButton1Click);
        button2.onClick.AddListener(OnButton2Click);
        backBtn.onClick.AddListener(OnBackBtnClick);

    }

    //main buttons
    public void OnButton1Click()
    {
        disableAllButtons();

        DialogueManagerGois.Chat.SetActive(false);
        DialogueManagerGois.User.SetActive(false);
        DialogueManagerGois.BrowserScreen.SetActive(true);

        enableAllButtons();
    }

    public void OnButton2Click()
    {
        disableAllButtons();

        StartCoroutine(DialogueManagerGois.win());
    }

    public void OnButton3Click()
    {
        disableAllButtons();

        DialogueManagerGois.BrowserScreen.SetActive(false);
        DialogueManagerGois.BrowserScreenFilled.SetActive(true);

        enableAllButtons();
    }

    public void OnButton4Click()
    {
        StartCoroutine(DialogueManagerGois.lose());
    }
    public void OnBackBtnClick()
    {
        disableAllButtons();

        if (DialogueManagerGois.BrowserScreen.activeSelf == true || DialogueManagerGois.BrowserScreenFilled.activeSelf == true)
        {
            // Switch back to the chat screen
            DialogueManagerGois.Chat.SetActive(true);
            DialogueManagerGois.User.SetActive(true);
            DialogueManagerGois.BrowserScreen.SetActive(false);
            DialogueManagerGois.BrowserScreenFilled.SetActive(false);

            // Enable all buttons
            enableAllButtons();
        }
    }

    //helper methods
    public void disableAllButtons()
    {
        button1.enabled = false;
        button2.enabled = false;
    }

    public void enableAllButtons()
    {
        button1.enabled = true;
        button2.enabled = true;
    }

}
