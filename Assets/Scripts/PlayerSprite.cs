using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public float animationSpeed;
    public Sprite[] upSprites, downSprites, rightSprites, leftSprites;
    int currentSpriteIndex;
    GameClock gameClock;
    float animationSpeedTimer;
    SpriteRenderer spriteRenderer;
    public Direction direction;
    Rigidbody2D rb;

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Start()
    {
        gameClock = FindObjectOfType<GameClock>();
    }

    private void Update()
    {
        CalculateAngleForAnim(Vector2.zero, rb.velocity.normalized);
        animationSpeedTimer += Time.deltaTime * gameClock.flowRate;
        if (animationSpeedTimer >= animationSpeed)
        {
            currentSpriteIndex++;
            if (currentSpriteIndex == upSprites.Length)
            {
                currentSpriteIndex = 0;
            }
            spriteRenderer.sprite = GetNextSprite();
            animationSpeedTimer = 0f;
        }
    }

    public void UpdateDirection(Direction dir)
    {
        direction = dir;
        spriteRenderer.sprite = GetNextSprite();
    }

    Sprite GetNextSprite()
    {
        switch (direction)
        {
            case Direction.Left:
                return leftSprites[currentSpriteIndex];
                break;
            case Direction.Right:
                return rightSprites[currentSpriteIndex];
                break;
            case Direction.Up:
                return upSprites[currentSpriteIndex];
                break;
            case Direction.Down:
                return downSprites[currentSpriteIndex];
                break;
            default:
                return null;
                break;
        }
    }


    private void CalculateAngleForAnim(Vector2 me, Vector2 target)
    {
        if (Vector2.Equals(Vector2.zero, target)) return;
        float angleBetween = AngleBetweenVector2(me, target);

        if (angleBetween >= 45 && angleBetween < 135)
        {
            UpdateDirection(Direction.Up);
        }
        else if (angleBetween >= 135 || angleBetween < -135)
        {
            UpdateDirection(Direction.Left);
        }
        else if (angleBetween >= -135 && angleBetween < -45)
        {
            UpdateDirection(Direction.Down);
        }
        else if (angleBetween >= -45 && angleBetween < 45)
        {
            UpdateDirection(Direction.Right);
        }
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}
