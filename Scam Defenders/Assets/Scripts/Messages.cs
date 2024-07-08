using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Messages : MonoBehaviour
{
    public void messageOnClick()
    {
        SceneManager.LoadScene("Chat");
    }
}
