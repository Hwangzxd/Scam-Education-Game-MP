using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRandomizer : MonoBehaviour
{
    public string[] shopEaseScenes = { "ShopEase", "ShopEase2", "ShopEase3" };
    public string[] birdyNetScenes = { "LoveScamChatNew1", "LoveScamChatNew2", "LoveScamChatNew3" };
    public string[] yabberChatINVSScenes = { "YabberChatINVS", "YabberChatINVS1", "YabberChatINVS2" };
    public string[] yabberChatGOISScenes = { "YabberChatGOIS", "YabberChatGOIS1", "YabberChatGOIS2" };

    public void LoadRandomSceneShopEase()
    {
        int randomIndex = Random.Range(0, shopEaseScenes.Length);
        SceneManager.LoadScene(shopEaseScenes[randomIndex]);
    }

    public void LoadRandomSceneBirdyNet()
    {
        int randomIndex = Random.Range(0, birdyNetScenes.Length);
        SceneManager.LoadScene(birdyNetScenes[randomIndex]);
    }

    public void LoadRandomSceneINVS()
    {
        int randomIndex = Random.Range(0, yabberChatINVSScenes.Length);
        SceneManager.LoadScene(yabberChatINVSScenes[randomIndex]);
    }

    public void LoadRandomSceneGOIS()
    {
        int randomIndex = Random.Range(0, yabberChatGOISScenes.Length);
        SceneManager.LoadScene(yabberChatGOISScenes[randomIndex]);
    }
}
