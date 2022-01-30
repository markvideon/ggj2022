using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{

    public bool playOnSpawn;
    public float minPitch, maxPitch, minVol, maxVol;
    public AudioClip forwardClip, backwardsClip;
    public AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (playOnSpawn) Play(true);
    }

    public void Play(bool forward)
    {
        source.pitch = Random.Range(minPitch, maxPitch);
        source.volume = Random.Range(minVol, maxVol);
        source.PlayOneShot(forward ? forwardClip : backwardsClip);
    }
}
