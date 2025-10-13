using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider _healthbarSlider; // Reference to the slider UI component
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    void Start()
    {
        // Initialize the slider's value to the current health (it's already 0-100 by default)
        _healthbarSlider.value = currentHealth;
    }

    // Method to update the health bar's value
    public void UpdateHealthBar(float currentHealth)
    {
        _healthbarSlider.value = currentHealth; // Directly set the slider value to current health
    }

    void Update()
    {
        // Detect key press for damage (K key)
        if (Input.GetKeyDown(KeyCode.K))
        {
            DealDamage(10f); // Damage value (10 can be adjusted as needed)
        }
    }

    // Method to deal damage and update health
    void DealDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // Ensure health doesn't go below 0
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Update the health bar
        UpdateHealthBar(currentHealth);
    }
}
