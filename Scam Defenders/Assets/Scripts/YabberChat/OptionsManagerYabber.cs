using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.WebRequestMethods;

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
        if(topImage.TryGetComponent<Image>(out Image image))
        {
            originalSprite = image.sprite;
        }
    }

    //for contacting independent financial advisor
    public void OnButton1Click()
    {
        disableAllButtons();
        textName.text = "Trusted Financial Advisor";
        YabberDialogueManager.HideAllOriginalMessages(); 
        StartCoroutine(YabberDialogueManager.ContactIndependentFinancialAdvisor());
        Debug.Log("Button 1 clicked");
    }

    //for visiting official financial regulator website (shows companies that does financial stuff)
    public void OnButton2Click()
    {
        disableAllButtons();
        btn2UI();
        Debug.Log("Button 2 clicked");

    }

    //UI for btn 2
    private void btn2UI()
    {
        user.SetActive(false);
        chat.SetActive(false);

        financialInstitutions.SetActive(true);
        lockIcon.SetActive(true);

        textName.text = "https://eservices.mas.gov.sg/fid/institution";

        if (topImage.TryGetComponent<Image>(out Image image))
        {
            image.sprite = btn2Sprite;
        }
        backBtn.enabled = true;
    }

    //for requesting official documentations
    public void OnButton3Click()
    {
        btn3Pressed = true;
        disableAllButtons();
        textName.text = "+65 9343 3432";
        YabberDialogueManager.HideAllAdvisorMessages();
        StartCoroutine(YabberDialogueManager.RequestOfficialDocumentations());
        Debug.Log("Button 3 clicked");
    }

    //for researching the company/fund on google
    public void OnButton4Click()
    {
        btn4Pressed = true;
        disableAllButtons();
        btn4UI();
        Debug.Log("Button 4 clicked");
    }

    //UI for btn 4
    private void btn4UI()
    {
        user.SetActive(false);
        chat.SetActive(false);

        lockIcon.SetActive(true);
        research.SetActive(true);
        searchBar.SetActive(true);
        loadIcon.SetActive(false);
        infoText.SetActive(false);
        textName.text = "https://google.com";

        if (topImage.TryGetComponent<Image>(out Image image))
        {
            image.sprite = btn4Sprite;
        }
    }

    //search bar btn for btn 4
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
        yield return new WaitForSeconds(3f); // Wait for 3 second before showing the next message
        infoText.SetActive(true);
        loadIcon.SetActive(false);
        backBtn.enabled = true;
    }

    //going back from the different buttons options (users can also just click on the buttons if shown)
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

    //when scenario is loading
    private void disableAllButtons()
    {
        button1.enabled = false;
        button2.enabled = false;
        button3.enabled = false;
        button4.enabled = false;
        backBtn.enabled = false;
    }

    //after scenario has been loaded
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
}

