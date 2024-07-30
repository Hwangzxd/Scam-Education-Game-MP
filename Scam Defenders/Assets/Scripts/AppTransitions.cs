using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppTransitions : MonoBehaviour
{
    public void EndTransition()
    {
        this.gameObject.SetActive(false);
    }
}
