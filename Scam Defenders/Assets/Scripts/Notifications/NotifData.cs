using System.Collections.Generic;
using UnityEngine;

public class NotifData : MonoBehaviour
{
    public static NotifData Instance; // Singleton instance

    public HashSet<int> shownNotifs = new HashSet<int>(); // Tracks activated notifications by index

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterShownNotif(int notifIndex)
    {
        shownNotifs.Add(notifIndex);
    }

    public void ClearShownNotifs()
    {
        shownNotifs.Clear();
        Debug.Log("All notifications have been shown and the set has been reset.");
    }

    public bool HasShownAllNotifs(int totalNotifsCount)
    {
        return shownNotifs.Count >= totalNotifsCount;
    }
}
