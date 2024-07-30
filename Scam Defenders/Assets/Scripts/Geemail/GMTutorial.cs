using UnityEngine;

public class GMTutorial : MonoBehaviour
{
    public GameObject[] guides;

    private int currentGuideIndex = 0;

    void Start()
    {
        for (int i = 0; i < guides.Length; i++)
        {
            guides[i].SetActive(i == 0); //Activates the first guide, deactivates the rest
        }
    }

    public void EnableNextGuide()
    {
        //Ensures current index is within array length
        if (currentGuideIndex < guides.Length)
        {
            //Deactivates the current guide
            guides[currentGuideIndex].SetActive(false);

            //Increments the index and activates the next guide if available
            currentGuideIndex++;

            if (currentGuideIndex < guides.Length)
            {
                guides[currentGuideIndex].SetActive(true);
            }
            else
            {
                //If no more guides, deactivate all
                for (int i = 0; i < guides.Length; i++)
                {
                    guides[i].SetActive(false);
                }
            }
        }
    }
}
