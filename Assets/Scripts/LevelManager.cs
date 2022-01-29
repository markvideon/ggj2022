using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int requiredKeyCount;
    public string nextLevelname;
    int currentKeyCount;
    bool portalOpen;
    public UnityEvent onLevelComplete, onLevelEnd;

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

    public void NextScene()
    {
        SceneManager.LoadScene(nextLevelname);
    }
}
