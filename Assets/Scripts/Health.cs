using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Health : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;
    public UnityEvent OnDeath;

    public bool showDebug;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnDeath.Invoke();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (showDebug)
            Handles.Label(transform.position, string.Format("{0}/{1}",  currentHealth, maxHealth));
    }
#endif
}