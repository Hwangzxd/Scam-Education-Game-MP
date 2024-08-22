using UnityEngine;

[CreateAssetMenu(fileName = "YabberData", menuName = "YabberData")]
public class YabberData : ScriptableObject
{
    public bool isINVSClicked;
    public bool isGOISClicked;

    // This method checks whether both notifications are clicked
    public bool INVSClicked()
    {
        return isINVSClicked;
    }

    public bool GOISClicked()
    {
        return isGOISClicked;
    }
}
