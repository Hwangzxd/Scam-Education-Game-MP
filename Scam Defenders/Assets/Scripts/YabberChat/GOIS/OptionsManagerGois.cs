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
    public Button searchBarBtn;

    public Button block;
    public Button reply;

    public TextMeshProUGUI textName;
    public TextMeshProUGUI searchText;

    public bool btn3Pressed = false;
    public bool btn4Pressed = false;

    public GameObject originalMessage;

    public GameObject lockIcon;
    public GameObject loadIcon;
    public GameObject infoText;
    public GameObject searchBar;
    public GameObject research;
    public GameObject user;
    public GameObject chat;
    public GameObject topImage;
    public GameObject financialInstitutions;

    public Sprite btn4Sprite;
    public Sprite btn2Sprite;

    // Add a boolean to track the current state
    private bool isChatActive = true;

    void Start()
    {
        // Assign listeners to the buttons
        button1.onClick.AddListener(OnButton1Click);
        button2.onClick.AddListener(OnButton2Click);
        button3.onClick.AddListener(OnButton3Click);
        button4.onClick.AddListener(OnButton4Click);
        backBtn.onClick.AddListener(OnBackBtnClick);
        block.onClick.AddListener(OnBlockButtonClick);
        reply.onClick.AddListener(OnReplyButtonClick);

        //InitialiseUI();
    }

    //void InitialiseUI()
    //{
    //    lockIcon.SetActive(false);
    //    research.SetActive(false);
    //    searchBar.SetActive(false);
    //    financialInstitutions.SetActive(false);

    //    user.SetActive(true);
    //    chat.SetActive(true);

    //    // Set the sprite to be the scammer
    //    if (topImage.TryGetComponent<Image>(out Image image))
    //    {
    //        originalSprite = image.sprite;
    //    }
    //}

    //main buttons
    public void OnButton1Click()
    {
        StartCoroutine(DialogueManagerGois.Scenario1());
    }

    public void OnButton2Click()
    {
        DialogueManagerGois.Scenario2();
    }

    public void OnButton3Click()
    {
        StartCoroutine(DialogueManagerGois.Scenario3());
    }

    public void OnButton4Click()
    {
        StartCoroutine(DialogueManagerGois.Scenario4());
    }

    public void OnBackBtnClick()
    {
        //Debug.Log("test");
        if (DialogueManagerGois.BrowserScreen.activeSelf == true)
        {
            //Debug.Log("test1");
            // Switch back to the chat screen
            DialogueManagerGois.ChatScreen.SetActive(true);
            DialogueManagerGois.BrowserScreen.SetActive(false);

            // Restore the transparency of OptionsPopUp to full
            CanvasGroup optionsCanvasGroup = DialogueManagerGois.OptionsPopUp.GetComponent<CanvasGroup>();
            if (optionsCanvasGroup == null)
            {
                optionsCanvasGroup = DialogueManagerGois.OptionsPopUp.AddComponent<CanvasGroup>();
            }
            optionsCanvasGroup.alpha = 1; // Set to full visibility

            // Enable all buttons
            enableAllButtons();
        }
    }

    //helper methods
    public void disableAllButtons()
    {
        button1.enabled = false;
        button2.enabled = false;
        button3.enabled = false;
        button4.enabled = false;
    }

    public void enableAllButtons()
    {
        button1.enabled = true;
        button2.enabled = true;
        button3.enabled = true;
        button4.enabled = true;
    }

    //deciding buttons
    public void OnBlockButtonClick()
    {
        disableAllButtons();
        reply.enabled = false;
        StartCoroutine(DialogueManagerGois.win1());
    }

    public void OnReplyButtonClick()
    {
        disableAllButtons();
        block.enabled = false;
        StartCoroutine(DialogueManagerGois.lose1());
    }
}
