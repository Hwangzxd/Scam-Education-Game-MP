using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRandomizer : MonoBehaviour
{
    public void LoadRandomScene()
    {
        string[] scenes = { "ShopEase", "ShopEase2", "ShopEase3" };

        int randomIndex = Random.Range(0, scenes.Length);
        SceneManager.LoadScene(scenes[randomIndex]);
    }
}
