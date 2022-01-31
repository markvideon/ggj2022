using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimationFrame : Frame
{
    public TowerAnimationFrame (Sprite sprite)
    {
        this.sprite = sprite;
    }
    public Sprite sprite;
}

public class TowerAnimation : BufferedState<TowerAnimation, TowerAnimationFrame>
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
        base.Start();
        gameClock = FindObjectOfType<GameClock>();
        SetOnRead((Frame) =>
        {
            spriteRenderer.sprite = Frame.sprite;
        });
        SetOnWrite(() =>
        {
            var newFrame = new TowerAnimationFrame(spriteRenderer.sprite);
            Record(newFrame);
        });
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void Update()
    {
        if (gameClock.flow == FlowDirection.backward) return;
        animationSpeedTimer += Time.deltaTime * gameClock.flowRate;
        if (animationSpeedTimer >= animationSpeed)
        {
            currentSpriteIndex++;
            if (currentSpriteIndex == sprites.Length)
            {
                currentSpriteIndex = 0;
            }
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            animationSpeedTimer = 0f;
        }
    }
}
