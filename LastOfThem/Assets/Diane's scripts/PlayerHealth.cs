using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100.0f;     // maximum health of the player
    public Slider healthSlider;          // reference to the UI slider for health

    private float currentHealth;         // current health of the player

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        healthSlider.value = currentHealth / maxHealth;
    }

    private void Die()
    {
        SceneManager.LoadScene("Lose_Screen");
    }
}
