using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnimationFrame : Frame
{
    public KeyAnimationFrame(Sprite sprite)
    {
        this.sprite = sprite;
    }
    public Sprite sprite;
}

public class KeyAnimation : BufferedState<TowerAnimation, TowerAnimationFrame>
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
                print(currentSpriteIndex);
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
