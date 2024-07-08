using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatPopUps : MonoBehaviour
{
    public GameObject winPop;
    public GameObject losePop;

    public GameObject winPoints; 
    //public GameObject losePoints;

    public void Report()
    {
        if (!losePop.activeSelf && !winPop.activeSelf)
        {
            winPop.SetActive(true);
        }
    }

    public void Apply()
    {
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

            winPoints.SetActive(true);
        } 

        if (!winPop.activeSelf && losePop.activeSelf)
        {
            losePop.SetActive(false);
            winPop.SetActive(false); 
             
            //losePoints.SetActive(true);
        }
    }
}
