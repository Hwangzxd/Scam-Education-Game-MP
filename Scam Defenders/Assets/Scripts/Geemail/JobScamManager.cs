using UnityEngine;

public class JobScamManager : MonoBehaviour
{
    public DropArea[] scamSigns; 

    void Start()
    {
        if (scamSigns == null || scamSigns.Length == 0)
        {
            scamSigns = FindObjectsOfType<DropArea>();
        }
    }

    void Update()
    {
        //GameStatus();
    }

    private void GameStatus()
    {
        int foundSignsCount = 0;

        //Check each scam sign to see if it's occupied
        foreach (DropArea dropArea in scamSigns)
        {
            if (dropArea.isOccupied)
            {
                foundSignsCount++;
            }
        }

        //Provide feedback based on the number of signs found
        switch (foundSignsCount)
        {
            case 0:
                Debug.Log("All signs missed.");
                break;
            case 1:
                Debug.Log("1 sign found, 2 signs missed.");
                break;
            case 2:
                Debug.Log("2 signs found, 1 sign missed.");
                break;
            case 3:
                Debug.Log("All signs found!");
                break;
        }
    }
}
