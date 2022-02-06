using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySFX : MonoBehaviour
{
    public AudioClip[] hitSounds;
    AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Play(int index)
    {
        index -= 1;
        if (index >= hitSounds.Length) index = hitSounds.Length - 1;
        audio.PlayOneShot(hitSounds[index]);
    }

}
