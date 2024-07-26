using UnityEngine;

public class DragAnim : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveDuration = 1f;
    public float pauseDuration = 0.5f;

    void Start()
    {
        // Start the looping animation
        MoveToA();
    }

    public void MoveToA()
    {
        LeanTween.move(gameObject, pointA.position, moveDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(MoveToB);
    }

    public void MoveToB()
    {
        LeanTween.move(gameObject, pointB.position, moveDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            // Pause for a bit at point B
            LeanTween.delayedCall(gameObject, pauseDuration, () =>
            {
                // Teleport back to point A after the pause
                transform.position = pointA.position;
                MoveToA();
            });
        });
    }
}
