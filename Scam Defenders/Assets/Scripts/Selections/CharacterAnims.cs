using System.Collections;
using UnityEngine;

public class CharacterAnims : MonoBehaviour
{
    [SerializeField] private float offscreenMovement = 1000f; // Amount to move offscreen
    [SerializeField] private float animationDuration = 0.5f; // Duration of the animation
    private float centerPosition = 269f; // Center position

    private Vector3 initialPosition;
    private Vector3 offscreenPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
        offscreenPosition = new Vector3(centerPosition + offscreenMovement, transform.localPosition.y, transform.localPosition.z);
    }

    public void MoveToCenter()
    {
        LeanTween.moveLocalX(gameObject, centerPosition, animationDuration);
    }

    public void MoveOffscreen(float direction)
    {
        // Direction determines if we move to the right or left
        // Move the character by `offscreenMovement` units in the specified direction
        LeanTween.moveLocalX(gameObject, initialPosition.x + direction * offscreenMovement, animationDuration);
    }
}
