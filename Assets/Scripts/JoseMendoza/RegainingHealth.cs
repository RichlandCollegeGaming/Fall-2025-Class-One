using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.JoseMendoza
{
    public class PlayerHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        private int currentHealth;

        void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't exceed maxHealth
        }

        void Die()
        {
            Debug.Log("Player has died!");
            // Implement player death logic (e.g., respawn, game over screen)
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }
    }
}