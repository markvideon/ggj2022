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
    public float animationSpeed, shootAnimationSpeed;
    public Sprite[] idleSprites;
    public Sprite[] shootSprites;
    public int shotSpriteIndex;
    SpriteRenderer spriteRenderer;
    int currentSpriteIndex;
    float animationSpeedTimer;
    GameClock gameClock;
    bool shooting;
    Turret turret;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        turret = GetComponentInParent<Turret>();
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
        if ((animationSpeedTimer >= shootAnimationSpeed && shooting) || (animationSpeedTimer >= animationSpeed && !shooting))
        {
            currentSpriteIndex++;
            if (shooting)
            {
                if (currentSpriteIndex == shootSprites.Length)
                {
                    currentSpriteIndex = 0;
                    shooting = false;
                    spriteRenderer.sprite = idleSprites[currentSpriteIndex];

                }
                else if (currentSpriteIndex == shotSpriteIndex)
                {
                    turret.Shoot();
                    spriteRenderer.sprite = shootSprites[currentSpriteIndex];
                }
                else
                {
                    spriteRenderer.sprite = shootSprites[currentSpriteIndex];
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

    public void ShootAnimation(bool shootImmediately)
    {
        shooting = true;
        if (shootImmediately)
        {
            currentSpriteIndex = shotSpriteIndex - 1;
        }
        else
        {
            currentSpriteIndex = -1;
        }
        

    }
}
