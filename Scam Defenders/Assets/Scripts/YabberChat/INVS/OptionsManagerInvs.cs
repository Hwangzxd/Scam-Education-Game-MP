using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsManagerInvs : MonoBehaviour
{
    public DialogueManagerInvs DialogueManagerInvs;

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
    public GameObject infoReal;
    public GameObject searchBar;
    public Animator searchBarAnimator;
    public GameObject research;
    public GameObject user;
    public GameObject chat;
    public GameObject topImage;
    public GameObject pfp;
    public GameObject financialInstitutions;

    public Sprite btn4Sprite;
    public Sprite btn2Sprite;
    private Sprite originalSprite;

    private Vector3 originalSearchBarScale; // Store the original scale
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

        // Store the original scale of the search bar
        if (searchBar != null)
        {
            originalSearchBarScale = searchBar.transform.localScale;
            //Debug.Log("Original Scale: " + originalSearchBarScale);
        }
    }

    void InitialiseUI()
    {
        lockIcon.SetActive(false);
        research.SetActive(false);
        searchBar.SetActive(false);
        financialInstitutions.SetActive(false);

        user.SetActive(true);
        chat.SetActive(true);

        // Set the sprite to be the scammer
        if (topImage.TryGetComponent<Image>(out Image image))
        {
            originalSprite = image.sprite;
        }
        pfp.SetActive(true);
    }

    // For contacting independent financial advisor
    public void OnButton1Click()
    {
        disableAllButtons();
        textName.text = "Trusted Financial Advisor";
        DialogueManagerInvs.HideAllOriginalMessages();
        StartCoroutine(DialogueManagerInvs.ContactIndependentFinancialAdvisor());
        Debug.Log("Button 1 clicked");
    }

    // For visiting official financial regulator website (shows companies that do financial stuff)
    public void OnButton2Click()
    {
        disableAllButtons();
        btn2UI();
        Debug.Log("Button 2 clicked");
    }

    // UI for btn 2
    private void btn2UI()
    {
        user.SetActive(false);
        chat.SetActive(false);

        financialInstitutions.SetActive(true);
        lockIcon.SetActive(true);
        pfp.SetActive(false);
        textName.text = "https://eservices.mas.gov.sg/fid/institution";

        if (topImage.TryGetComponent<Image>(out Image image))
        {
            image.sprite = btn2Sprite;
        }
        backBtn.enabled = true;
    }

    // For requesting official documentations
    public void OnButton3Click()
    {
        btn3Pressed = true;
        disableAllButtons();
        if (SceneManager.GetActiveScene().name == "YabberChatINVS")
        {
            textName.text = "+65 9343 3432";
        }
        else if (SceneManager.GetActiveScene().name == "YabberChatINVS1")
        {
            textName.text = "+65 9123 4567";
        }
        else if (SceneManager.GetActiveScene().name == "YabberChatINVS2")
        {
            textName.text = "DBS Bank";
        }

        DialogueManagerInvs.HideAllAdvisorMessages();
        StartCoroutine(DialogueManagerInvs.RequestOfficialDocumentations());
        Debug.Log("Button 3 clicked");
    }

    // For researching the company/fund on Google
    public void OnButton4Click()
    {
        btn4Pressed = true;
        disableAllButtons();
        btn4UI();
        Debug.Log("Button 4 clicked");
    }

    // UI for btn 4
    private void btn4UI()
    {
        user.SetActive(false);
        chat.SetActive(false);

        lockIcon.SetActive(true);
        research.SetActive(true);
        searchBar.SetActive(true);
        loadIcon.SetActive(false);
        infoText.SetActive(false);
        pfp.SetActive(false);
        textName.text = "https://google.com";

        if (topImage.TryGetComponent<Image>(out Image image))
        {
            image.sprite = btn4Sprite;
        }
    }

    // Search bar button for btn 4
    public void OnSearchBarClick()
    {
        if (btn4Pressed)
        {

            if (SceneManager.GetActiveScene().name == "YabberChatINVS" || SceneManager.GetActiveScene().name == "YabberChatINVS1")
            {
                if (SceneManager.GetActiveScene().name == "YabberChatINVS")
                {
                    searchText.text = "SG Wealth Management";
                }
                else if (SceneManager.GetActiveScene().name == "YabberChatINVS1")
                {
                    searchText.text = "Apex Global Investments";
                }
            }
            else if (SceneManager.GetActiveScene().name == "YabberChatINVS2")
            {
                searchText.text = "DBS Multiplier Account";
            }

            infoText.SetActive(false);
            infoReal.SetActive(false);
            loadIcon.SetActive(true);
            loadCoroutine = ResearchCompany();
            StartCoroutine(loadCoroutine);

            // Stop the search bar animation
            if (searchBarAnimator != null)
            {
                searchBarAnimator.enabled = false;
            }

            // Reset the scale to the original
            ResetSearchBarScale();
        }
    }

    private void ResetSearchBarScale()
    {
        // Use the stored original scale
        if (searchBar != null)
        {
            searchBar.transform.localScale = originalSearchBarScale;
            Debug.Log("Reset Scale: " + originalSearchBarScale);
        }
    }

    private IEnumerator ResearchCompany()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds before showing the information
        if (SceneManager.GetActiveScene().name == "YabberChatINVS" || SceneManager.GetActiveScene().name == "YabberChatINVS1")
        {
            infoText.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "YabberChatINVS2")
        {
            infoReal.SetActive(true);
        }

        loadIcon.SetActive(false);
        backBtn.enabled = true;
    }

    // Going back from the different button options (users can also just click on the buttons if shown)
    public void OnBackBtnClick()
    {
        disableAllButtons();
        if (SceneManager.GetActiveScene().name == "YabberChatINVS")
        {
            textName.text = "+65 9343 3432";
        }
        else if (SceneManager.GetActiveScene().name == "YabberChatINVS1")
        {
            textName.text = "+65 9123 4567";
        }
        else if (SceneManager.GetActiveScene().name == "YabberChatINVS2")
        {
            textName.text = "DBS Bank";
        }
        ResetUI();
        DialogueManagerInvs.HideAllAdvisorMessages();
        if (btn3Pressed)
        {
            DialogueManagerInvs.ShowAllOriginalMessages();
        }
        else
        {
            originalMessage.SetActive(true);
        }
        enableAllButtons();
    }

    // When scenario is loading
    private void disableAllButtons()
    {
        button1.enabled = false;
        button2.enabled = false;
        button3.enabled = false;
        button4.enabled = false;
        backBtn.enabled = false;
    }

    // After scenario has been loaded
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
        pfp.SetActive(true);
    }

    public void OnBlockButtonClick()
    {
        disableAllButtons();
        reply.enabled = false;
        if (SceneManager.GetActiveScene().name == "YabberChatINVS" || SceneManager.GetActiveScene().name == "YabberChatINVS1")
        {
            StartCoroutine(DialogueManagerInvs.win1());
        }
        else if (SceneManager.GetActiveScene().name == "YabberChatINVS2")
        {
            StartCoroutine(DialogueManagerInvs.lose1());
        }
    }

    public void OnReplyButtonClick()
    {
        if (SceneManager.GetActiveScene().name == "YabberChatINVS" || SceneManager.GetActiveScene().name == "YabberChatINVS1")
        {
            StartCoroutine(DialogueManagerInvs.lose1());
        }
        else if (SceneManager.GetActiveScene().name == "YabberChatINVS2")
        {
            StartCoroutine(DialogueManagerInvs.win1());
        }
    }
}
