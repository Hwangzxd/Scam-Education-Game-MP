using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public static LevelLoader instance;
    public GameObject levelLoader;

    public float transitionTime = 1f;
    //public float destroyDelay = 0.2f;

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Make sure timeText is not destroyed
            if (levelLoader != null)
            {
                DontDestroyOnLoad(levelLoader.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    LoadNextLevel();
        //}
    }

    public void LoadNextLevel()
    {
        //SceneManager.LoadScene("ShopEaseSearch");
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("ShopEaseChoose");

        //yield return new WaitForSeconds(destroyDelay);
        // Destroy the LevelLoader GameObject after the scene has loaded
        Destroy(levelLoader);
    }
}
