using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    public int requiredKeyCount;
    public string nextLevelname;
    int currentKeyCount;
    bool portalOpen;
    public UnityEvent onLevelComplete, onLevelEnd;
    PlayerMovement pm;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    public void AddKey()
    {
        currentKeyCount++;
        if (currentKeyCount >= requiredKeyCount)
        {
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        if (portalOpen) return;
        portalOpen = true;
        onLevelComplete.Invoke();
    }

    public void EndLevel()
    {
        onLevelEnd.Invoke();
    }

    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        if (nextLevelname == "")
        {
            pm.OnWin();
        }
        else
        {
            SceneManager.LoadScene(nextLevelname);
        }
        
    }
}
