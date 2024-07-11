using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatPopUps : MonoBehaviour
{
    public GameObject winPop;
    public GameObject losePop;

    public void Report()
    {
        Debug.Log("Report clicked");

        if (!losePop.activeSelf && !winPop.activeSelf)
        {
            winPop.SetActive(true); 
        }
    }

    public void Apply()
    {
        Debug.Log("Reply clicked");

        if (!winPop.activeSelf && !losePop.activeSelf)
        {
            losePop.SetActive(true);
        }
    } 
     
    public void Continue()
    {
        if (winPop.activeSelf && !losePop.activeSelf)
        {
            losePop.SetActive(false);
            winPop.SetActive(false);
        } 

        if (!winPop.activeSelf && losePop.activeSelf)
        {
            losePop.SetActive(false);
            winPop.SetActive(false); 
        }
    }
}
