using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button backButton;

    public void Start()
    {
        backButton = GetComponent<Button>();
        backButton.onClick.AddListener(SceneHistoryManager.Instance.Back);
    }
}
