using UnityEngine;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    // Called when the script instance is being loaded
    void Awake()
    {
        // Ensure this GameObject is not destroyed on load
        DontDestroyOnLoad(gameObject);
    }
}
