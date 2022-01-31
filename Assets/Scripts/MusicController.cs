using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameClock = FindObjectOfType<GameClock>();
        gameClock.AddToOnChangeFlow(ChangeDirection);
        ChangeDirection();
    }

    void OnSceneUnloaded(Scene scene)
    {
        gameClock.RemoveFromOnChangeFlow(ChangeDirection);
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
