using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnimation : MonoBehaviour
{
    public float animationSpeed;
    public Sprite activeIdleSprite;
    public Sprite[] activateSprites;
    SpriteRenderer spriteRenderer;
    int currentSpriteIndex;
    float animationSpeedTimer;
    GameClock gameClock;
    bool animating;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gameClock = FindObjectOfType<GameClock>();

    }


    private void Update()
    {
        if (!animating) return;
        animationSpeedTimer += Time.deltaTime * gameClock.flowRate;
        if (animationSpeedTimer >= animationSpeed)
        {
            currentSpriteIndex++;
            if (currentSpriteIndex >= activateSprites.Length)
            {
                animating = false;
                spriteRenderer.sprite = activeIdleSprite;
            }
            else
            {
                spriteRenderer.sprite = activateSprites[currentSpriteIndex];
            }
            animationSpeedTimer = 0f;
        }
    }


    public void PlayAnimation()
    {
        animating = true;
        animationSpeedTimer = animationSpeed;
    }
}
