using UnityEngine;

public class ReqManager : MonoBehaviour
{
    public YabberData yabberData;

    public GameObject GOISmsg;
    public GameObject INVSmsg;

    public GameObject noMsgs;

    public void Start()
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
        }
        else
        {
            noMsgs.SetActive(true);
        }

    }
    public void resetBools()
    {
        yabberData.isGOISClicked = false;
        yabberData.isINVSClicked = false;
    }
}
