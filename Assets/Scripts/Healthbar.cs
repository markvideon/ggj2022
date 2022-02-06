using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public GameObject[] hearts;
    public int health;

    public void UpdateHearts(int newHealth)
    {
        if (newHealth > hearts.Length) return;
        foreach (var item in hearts)
        {
            item.SetActive(false);
        }
        for (int i = 0; i < newHealth; i++)
        {
            hearts[i].SetActive(true);
        }
        health = newHealth;
    }
}
