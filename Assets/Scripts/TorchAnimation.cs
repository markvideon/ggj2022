using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchAnimation : MonoBehaviour
{
    public float animationSpeed;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    int currentSpriteIndex;
    float animationSpeedTimer;
    GameClock gameClock;

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
        animationSpeedTimer += Time.deltaTime * gameClock.flowRate;
        if (animationSpeedTimer >= animationSpeed)
        {
            if (gameClock.flow == FlowDirection.forward)
            {
                currentSpriteIndex++;
                if (currentSpriteIndex >= sprites.Length)
                {
                    currentSpriteIndex = 0;
                }
            }
            else
            {
                currentSpriteIndex--;
                if (currentSpriteIndex <= -1)
                {
                    currentSpriteIndex = sprites.Length - 1;
                }
            }
            spriteRenderer.sprite = sprites[currentSpriteIndex];

            animationSpeedTimer = 0f;
        }
    }

}
