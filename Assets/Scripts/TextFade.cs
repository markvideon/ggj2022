using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    public float fadeSpeed;
    CanvasGroup canvasGroup;
    bool fading;
    bool fadeIn;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    private void Start()
    {
        Fade(false);
    }

    private void Update()
    {
        if (fading)
        {
            if (fadeIn)
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 0f, Time.deltaTime * fadeSpeed);
                if (canvasGroup.alpha == 0f) fading = false;
            }
            else
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 1f, Time.deltaTime * fadeSpeed);
                if (canvasGroup.alpha == 1f) fading = false;
            }
        }
    }

    public void Fade(bool fadeInPlease)
    {
        fading = true;
        fadeIn = fadeInPlease;
    }
}
