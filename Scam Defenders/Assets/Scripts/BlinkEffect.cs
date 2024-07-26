using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    public Color startColor = Color.green;
    public Color endColor = Color.black; 

    [Range(0, 10)]
    public float speed = 1;

    //private SpriteRenderer sprite;
    public Image image;

    void Awake()
    {
        //sprite = GetComponent<SpriteRenderer>(); 
        image = GetComponent<Image>();
    }

    void Update()
    {
        //sprite.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1)); 
        image.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }
}