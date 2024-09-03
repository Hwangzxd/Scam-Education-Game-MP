using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    private Button backButton;

    public void Start()
    {
        backButton = GetComponent<Button>();
        backButton.onClick.AddListener(OnBackButtonPressed);
    }

    private void OnBackButtonPressed()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Home")
        {
            SceneHistoryManager.Instance.ResetHistory();
        }
        else
        {
            SceneHistoryManager.Instance.Back();
        }
    }
}
