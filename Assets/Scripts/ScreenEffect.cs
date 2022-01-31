using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffect : MonoBehaviour
{
    public GameObject screenToEnable;
    GameClock gameClock;

    private void Start()
    {
        gameClock = FindObjectOfType<GameClock>();
    }

    private void Update()
    {
        switch (gameClock.flow)
        {
            case FlowDirection.error:
                break;
            case FlowDirection.forward:
                screenToEnable.SetActive(false);
                break;
            case FlowDirection.backward:
                screenToEnable.SetActive(true);
                break;
        }
    }
}
