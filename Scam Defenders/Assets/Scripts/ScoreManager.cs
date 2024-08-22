using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private bool ptsGiven = false;

    void Awake()
    {
        ptsGiven = false;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    { 
        if (GMData.Instance != null)
        {
            if (GMData.Instance.GetWin() && !ptsGiven)
            {
                RepData.Instance.PlusReputation(10);
                GMData.Instance.SetWin(false); // Reset win state
                ptsGiven = true;
            }
            else if (GMData.Instance.GetLose() && !ptsGiven)
            {
                RepData.Instance.MinusReputation(10);
                GMData.Instance.SetLose(false); // Reset lose state
                ptsGiven = true;
            }
        }
    }
}
