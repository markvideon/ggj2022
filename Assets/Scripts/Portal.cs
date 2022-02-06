using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public AudioClip enterSound;
    LevelManager levelManager;
    AudioSource audio;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        PlayerMovement pm = collision.gameObject.GetComponentInParent<PlayerMovement>();
        if (pm)
        {
            levelManager.EndLevel();
            audio.PlayOneShot(enterSound);
        }
    }
}
