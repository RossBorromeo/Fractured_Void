using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public static FlashEffect Instance; // singleton 

    public Color startColor = Color.yellow;
    public Color endColor = Color.white;
    [Range(0, 10)] public float speed = 1f;// blink speed
    public float blinkDuration = 5f;//blink duratiion

    private Image imgComp;
    private bool isBlinking = false;
    private Color originalColor;

    void Awake()
    {
        Instance = this;
        imgComp = GetComponent<Image>();
        originalColor = imgComp.color;
    }

    void Update()
    {
        if (isBlinking)
        {
            imgComp.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
        }
    }

    public void StartBlinking()
    {
        isBlinking = true;
        CancelInvoke(nameof(StopBlinking));
        Invoke(nameof(StopBlinking), blinkDuration);
    }

    public void StopBlinking()
    {
        isBlinking = false;
        imgComp.color = originalColor;
    }
}

