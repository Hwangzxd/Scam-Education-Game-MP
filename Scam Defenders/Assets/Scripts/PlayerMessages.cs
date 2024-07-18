using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMessages : MonoBehaviour
{
    public void Start()
    {
       transform.localScale = Vector3.zero; // Set initial scale
    }
    private void OnEnable()
    {
        transform.LeanScale(Vector3.one, 0.5f).setEase(LeanTweenType.easeOutBounce);
    }
}
