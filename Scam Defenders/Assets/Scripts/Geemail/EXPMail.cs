using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPMail : MonoBehaviour
{
    public GameObject explainScreen;  
    public GameObject postCanvas;
    public GameObject timerCanvas;
    public GameObject inventory;

    public void AfterMailMG()
    {
        explainScreen.gameObject.SetActive(true); 

        postCanvas.gameObject.SetActive(false); 
        timerCanvas.gameObject.SetActive(false); 
        inventory.gameObject.SetActive(false);  
    } 
} 

