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
    public bool takeDamage = true;
    public bool updateUI;
    Healthbar healthBar;
    public SFX onHitSFX;

    public bool showDebug;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        if (updateUI)
        {
            healthBar = FindObjectOfType<Healthbar>();
            if (healthBar.health > 0)
            {
                currentHealth = healthBar.health;
            }
            healthBar.UpdateHearts(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!takeDamage) return;
        currentHealth -= damage;
        print(currentHealth);
        if (updateUI) healthBar.UpdateHearts(currentHealth);
        if (onHitSFX) onHitSFX.Play(true);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (updateUI) Destroy(healthBar.transform.root.gameObject);
        OnDeath.Invoke();
    }

    public void SetInvincible(bool invincible)
    {
        takeDamage = !invincible;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (showDebug)
            Handles.Label(transform.position, string.Format("{0}/{1}", currentHealth, maxHealth));
    }
#endif
}
