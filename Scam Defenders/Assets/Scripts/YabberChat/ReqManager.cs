using UnityEngine;
using UnityEngine.SceneManagement;

public class ReqManager : MonoBehaviour
{
    public YabberData yabberData;

    public GameObject GOISmsg;
    public GameObject INVSmsg;
    public GameObject noMsgs;

    void Start()
    {
        GOISmsg.SetActive(false);
        INVSmsg.SetActive(false);

        if (yabberData.INVSClicked())
        {
            INVSmsg.SetActive(true);
        }
        else if (yabberData.GOISClicked())
        {
            GOISmsg.SetActive(true);

            // Retrieve and log the scenario index from YabberData
            int scenarioIndex = yabberData.GetGOISScenarioIndex();
            Debug.Log("Stored GOIS Scenario Index: " + scenarioIndex);
        }
        else
        {
            noMsgs.SetActive(true);
        }
    }

    // Method to load the GOIS scene based on the stored scenario index
    public void OnMsgClick()
    {
        int scenarioIndex = yabberData.GetGOISScenarioIndex();
        Debug.Log("OnMsgClick: Scenario Index = " + scenarioIndex);
        LoadGOISScene(scenarioIndex);
    }

    public void LoadGOISScene(int scenarioIndex)
    {
        switch (scenarioIndex)
        {
            case 0:
                SceneManager.LoadScene("YabberChatGOIS");
                Debug.Log("Loading Scene: YabberChatGOIS (case 0)");
                break;
        }
    }

    public void resetBools()
    {
        yabberData.isGOISClicked = false;
        yabberData.isINVSClicked = false;
    }

}