using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManagerYabber : MonoBehaviour
{
    public DialogueManagerYabber YabberDialogueManager;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button backBtn;
    public Button searchBarBtn;

    public TextMeshProUGUI textName;
    public TextMeshProUGUI searchText;

    public bool btn3Pressed = false;

    public GameObject originalMessage;

    public GameObject lockIcon;
    public GameObject loadIcon;
    public GameObject infoText;
    public GameObject research;
    public GameObject user;
    public GameObject chat;
    public GameObject topImage;

    public Sprite newSprite;

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

        InitialiseUI();
    }

    void InitialiseUI()
    {
        lockIcon.SetActive(false);
        research.SetActive(false);
        user.SetActive(true);
        chat.SetActive(true);

        if(topImage.TryGetComponent<Image>(out Image image))
        {
            originalSprite = image.sprite;
        }
    }

    public void OnButton1Click()
    {
        disableAllButtons();
        textName.text = "Trusted Financial Advisor";
        YabberDialogueManager.HideAllOriginalMessages(); 
        StartCoroutine(YabberDialogueManager.ContactIndependentFinancialAdvisor());
        Debug.Log("Button 1 clicked");
    }

    public void OnButton2Click()
    {
        disableAllButtons();
        Debug.Log("Button 2 clicked");

    }

    public void OnButton3Click()
    {
        btn3Pressed = true;
        disableAllButtons();
        textName.text = "+65 9343 3432";
        YabberDialogueManager.HideAllAdvisorMessages();
        StartCoroutine(YabberDialogueManager.RequestOfficialDocumentations());
        Debug.Log("Button 3 clicked");
    }

    public void OnButton4Click()
    {
        disableAllButtons();
        btn4UI();
        // Action for button 4
        Debug.Log("Button 4 clicked");
    }

    public void OnSearchBarClick()
    {
        searchText.text = "SG Wealth Management";
        loadIcon.SetActive(true);
        loadCoroutine = ResearchCompany();
        StartCoroutine(loadCoroutine);
        
    }

    public void OnBackBtnClick()
    {
        disableAllButtons();
        textName.text = "+65 9343 3432";
        ResetUI();
        YabberDialogueManager.HideAllAdvisorMessages();
        if (btn3Pressed)
        {
            YabberDialogueManager.ShowAllOriginalMessages();   
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

    private void btn4UI()
    {
        user.SetActive(false);
        chat.SetActive(false);

        lockIcon.SetActive(true);
        research.SetActive(true);
        loadIcon.SetActive(false);
        infoText.SetActive(false);
        textName.text = "https://google.com";

        if (topImage.TryGetComponent<Image>(out Image image))
        {
            image.sprite = newSprite;
        }
    }

    private IEnumerator ResearchCompany()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 second before showing the next message
        infoText.SetActive(true);
        loadIcon.SetActive(false);
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

        if (topImage.TryGetComponent<Image>(out Image image))
        {
            image.sprite = originalSprite;
        }
    }
}

