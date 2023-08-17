using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{

    public Sprite[] sprites;
    public float spritePerSecond = 6;
    public bool loop = true;
    public bool destroyOnEnd = false;

    private int index = 0;
    private Image image;
    private float timeSinceUpdate = 0;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (!loop && index == sprites.Length) return;
        timeSinceUpdate += Time.deltaTime;
        if (timeSinceUpdate < (1/spritePerSecond)) return;
        image.sprite = sprites[index];
        timeSinceUpdate = 0;
        index++;
        if (index >= sprites.Length)
        {
            if (loop) index = 0;
            if (destroyOnEnd) Destroy(gameObject);
        }
    }
}