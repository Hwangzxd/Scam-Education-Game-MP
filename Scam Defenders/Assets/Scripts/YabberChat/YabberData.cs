using UnityEngine;

[CreateAssetMenu(fileName = "YabberData", menuName = "YabberData")]
public class YabberData : ScriptableObject
{
    public bool isINVSClicked;
    public bool isGOISClicked;
    public int goisScenarioIndex;  // Store the index of the GOIS scenario

    // This method checks whether both notifications are clicked
    public bool INVSClicked()
    {
        return isINVSClicked;
    }

    public bool GOISClicked()
    {
        return isGOISClicked;
    }

    // Set the GOIS scenario index
    public void SetGOISScenarioIndex(int index)
    {
        goisScenarioIndex = index;
    }

    // Get the GOIS scenario index
    public int GetGOISScenarioIndex()
    {
        return goisScenarioIndex;
    }
}
