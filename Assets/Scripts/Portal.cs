using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        PlayerMovement pm = collision.gameObject.GetComponentInParent<PlayerMovement>();
        if (pm)
        {
            levelManager.EndLevel();
        }
    }
}
