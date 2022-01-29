using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public float minPitch;
    GameClock gameClock;
    public AudioSource forwardMusic, backwardMusic;
    bool forward;

    private void Awake()
    {
        backwardMusic.volume = 0;
    }

    void Start()
    {
        gameClock = FindObjectOfType<GameClock>();
    }

    [ContextMenu("Switch")]
    public void ToggleDirection()
    {
        forward = !forward;
        ChangeDirection(forward);
    }

    private void ChangeDirection(bool goForward)
    {
        forwardMusic.volume = goForward ? 1f : 0f;
        backwardMusic.volume = goForward ? 0f : 1f;
        forward = goForward;
    }
}
