using UnityEngine;

public class GMData : MonoBehaviour
{
    public static GMData Instance { get; private set; }

    public int miniGameScore;
    public bool win;
    public bool lose;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure this object persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void SetScore(int score)
    {
        miniGameScore = score;
        Debug.Log("GMData Score Set: " + miniGameScore); // Debug log
    }

    public int GetScore()
    {
        Debug.Log("GMData Score Retrieved: " + miniGameScore); // Debug log
        return miniGameScore;
    }

    public void SetWin(bool value)
    {
        win = value;
    }

    public bool GetWin()
    {
        return win;
    }

    public void SetLose(bool value)
    {
        lose = value;
    }

    public bool GetLose()
    {
        return lose;
    }
}
