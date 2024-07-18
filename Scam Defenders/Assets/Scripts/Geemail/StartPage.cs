using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPage : MonoBehaviour
{
    public GameObject startPageBg;

    public void StartMiniGame()
    { 
        startPageBg.SetActive(false);
    }
}
