using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.WebRequestMethods;

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
    private Sprite originalSprite;

    private IEnumerator loadCoroutine;

    void Start()
    {
        // Assign listeners to the buttons
        button1.onClick.AddListener(OnButton1Click);
        button2.onClick.AddListener(OnButton2Click);
        button3.onClick.AddListener(OnButton3Click);
        button4.onClick.AddListener(OnButton4Click);
        backBtn.onClick.AddListener(OnBackBtnClick);
        searchBarBtn.onClick.AddListener(OnSearchBarClick);
        block.onClick.AddListener(OnBlockButtonClick);
        reply.onClick.AddListener(OnReplyButtonClick);

        InitialiseUI();
    }

    void InitialiseUI()
    {
        lockIcon.SetActive(false);
        research.SetActive(false);
        searchBar.SetActive(false);
        financialInstitutions.SetActive(false);

        user.SetActive(true);
        chat.SetActive(true);

        //set the sprite to be the scammer
        if (topImage.TryGetComponent<Image>(out Image image))
        {
            originalSprite = image.sprite;
        }
    }

    public void OnButton1Click()
    {

    }

    public void OnButton2Click()
    {

    }


    public void OnButton3Click()
    {
        StartCoroutine(DialogueManagerGois.Scenario1());
    }

    public void OnButton4Click()
    {

    }


    public void OnSearchBarClick()
    {
        if (btn4Pressed)
        {
            searchText.text = "SG Wealth Management";
            loadIcon.SetActive(true);
            loadCoroutine = ResearchCompany();
            StartCoroutine(loadCoroutine);
        }
    }

    private IEnumerator ResearchCompany()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 second before showing the information
        infoText.SetActive(true);
        loadIcon.SetActive(false);
        backBtn.enabled = true;
    }

    public void OnBackBtnClick()
    {
        disableAllButtons();
        textName.text = "+65 9343 3432";
        ResetUI();
        DialogueManagerGois.HideAllAdvisorMessages();
        if (btn3Pressed)
        {
            DialogueManagerGois.ShowAllOriginalMessages();
        }
        else
        {
            originalMessage.SetActive(true);
        }
        enableAllButtons();
    }

    private void disableAllButtons()
    {
        button1.enabled = false;
        button2.enabled = false;
        button3.enabled = false;
        button4.enabled = false;
        backBtn.enabled = false;
    }

    public void enableAllButtons()
    {
        button1.enabled = true;
        button2.enabled = true;
        button3.enabled = true;
        button4.enabled = true;
        backBtn.enabled = true;
    }

    private void ResetUI()
    {
        user.SetActive(true);
        chat.SetActive(true);
        lockIcon.SetActive(false);
        research.SetActive(false);
        loadIcon.SetActive(false);
        infoText.SetActive(false);
        searchBar.SetActive(false);
        financialInstitutions.SetActive(false);

        if (topImage.TryGetComponent<Image>(out Image image))
        {
            image.sprite = originalSprite;
        }
    }

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