using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAnimation : MonoBehaviour
{
    public float animationSpeed, spawnAnimationSpeed;
    public Sprite[] idleSprites;
    public Sprite[] spawnSprites;
    SpriteRenderer spriteRenderer;
    int currentSpriteIndex;
    float animationSpeedTimer;
    GameClock gameClock;
    bool spawning = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationSpeedTimer = spawnAnimationSpeed;
        currentSpriteIndex = -1;
    }

    private void Start()
    {
        gameClock = FindObjectOfType<GameClock>();
    }

    private void Update()
    {
        animationSpeedTimer += Time.deltaTime * gameClock.flowRate;
        if ((animationSpeedTimer >= spawnAnimationSpeed && spawning) || (animationSpeedTimer >= animationSpeed && !spawning))
        {
            currentSpriteIndex++;
            if (spawning)
            {
                if (currentSpriteIndex == spawnSprites.Length)
                {
                    currentSpriteIndex = 0;
                    spawning = false;
                    spriteRenderer.sprite = idleSprites[currentSpriteIndex];
                }
                else
                {
                    spriteRenderer.sprite = spawnSprites[currentSpriteIndex];
                }
            }
            else
            {
                if (currentSpriteIndex == idleSprites.Length)
                {
                    currentSpriteIndex = 0;
                }
                spriteRenderer.sprite = idleSprites[currentSpriteIndex];
            }

            animationSpeedTimer = 0f;
        }
    }
}
