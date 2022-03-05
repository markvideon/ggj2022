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
    KeySFX keySFX;
    FTBManager ftb;

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        keySFX = GetComponent<KeySFX>();
        ftb = FindObjectOfType<FTBManager>();
    }

    public void AddKey()
    {
        currentKeyCount++;
        keySFX.Play(currentKeyCount);
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
        StartCoroutine(NextSceneCoroutine());
    }

    IEnumerator NextSceneCoroutine()
    {
        pm.canMove = false;
        ftb.Fade(false);
        yield return new WaitForSeconds(2f);
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
