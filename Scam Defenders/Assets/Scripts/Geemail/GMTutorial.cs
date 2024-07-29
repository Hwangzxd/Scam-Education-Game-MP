using UnityEngine;

public class GMTutorial : MonoBehaviour
{
    public GameObject[] guides;

    private int currentGuideIndex = 0;

    void Start()
    {
        // Initialize the guides' active states
        for (int i = 0; i < guides.Length; i++)
        {
            guides[i].SetActive(i == 0); // Activate the first guide, deactivate the rest
        }
    }

    public void EnableNextGuide()
    {
        // Ensure current index is within bounds
        if (currentGuideIndex < guides.Length - 1)
        {
            // Deactivate current guide
            guides[currentGuideIndex].SetActive(false);

            // Increment the index and activate the next guide
            currentGuideIndex++;
            guides[currentGuideIndex].SetActive(true);
        }
    }
}
