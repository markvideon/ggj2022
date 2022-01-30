using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public float minPitch;
    GameClock gameClock;
    public AudioClip forwardSFX, backwardSFX;
    public AudioSource forwardMusic, backwardMusic, SFXSource;
    bool forward;

    private void Awake()
    {
        backwardMusic.volume = 0;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        gameClock = FindObjectOfType<GameClock>();
        gameClock.AddToOnChangeFlow(ChangeDirection);
    }

    private void OnDestroy()
    {
        gameClock.RemoveFromOnChangeFlow(ChangeDirection);
    }


    private void ChangeDirection()
    {

        bool goForward = false;
        bool exit = false;
        switch (gameClock.flow)
        {
            case FlowDirection.error:
                exit = true;
                return;
            case FlowDirection.forward:
                goForward = true;
                if (forward == goForward) exit = true;
                break;
            case FlowDirection.backward:
                goForward = false;
                if (forward == goForward) exit = true;
                break;
        }
        if (exit) return;
        print(goForward);
        forwardMusic.volume = goForward ? 1f : 0f;
        backwardMusic.volume = goForward ? 0f : 1f;
        //SFXSource.PlayOneShot(goForward ? forwardSFX : backwardSFX);
        forward = goForward;
    }
}
