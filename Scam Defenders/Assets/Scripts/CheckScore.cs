using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckScore : MonoBehaviour
{
    void Update()
    {
        if (RepData.Instance.GetReputation() == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
