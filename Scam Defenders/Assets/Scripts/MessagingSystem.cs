using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagingSystem : MonoBehaviour
{
    public List<GameObject> npcMessages; // The object you want to activate
    //public GameObject dialogueOptions;
    public Button optionButton; // The button that triggers the activation
    public float delay = 2f; // Delay in seconds
    public float animationDuration = 0.5f; // Duration of the size-in animation
    public Vector3 initialScale = Vector3.zero; // Initial scale for the size-in animation

    void Start()
    {
        if (optionButton != null)
        {
            optionButton.onClick.AddListener(StartActivationProcess);
        }

        //foreach (GameObject obj in npcMessages) 
        //{
        //    transform.localScale = Vector2.zero;
        //}
    }

    void StartActivationProcess()
    {
        StartCoroutine(ActivateAfterDelay());
    }

    IEnumerator ActivateAfterDelay()
    {
        foreach (GameObject obj in npcMessages)
        {
            yield return new WaitForSeconds(delay);
            if (obj != null)
            {
                //transform.LeanScale(Vector2.one, 0.8f);

                //obj.SetActive(true);

                ActivateAndAnimate(obj);
            }
        }

        //yield return new WaitForSeconds(delay);
        //if (dialogueOptions != null)
        //{
        //    dialogueOptions.SetActive(true);
        //}
    }

    void ActivateAndAnimate(GameObject obj)
    {
        // Make sure the object is not set to zero scale initially
        obj.SetActive(true);
        obj.transform.localScale = Vector3.one * 0.1f; // Start from a small but non-zero scale

        // Animate scaling to normal size
        LeanTween.scale(obj, Vector3.one, animationDuration)
            .setEase(LeanTweenType.easeOutBounce);
    }

}