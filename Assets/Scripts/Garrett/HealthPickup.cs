using System.Collections;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int damageAmount = -10;  // Amount of damage to deal to the player

    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Find the Healthbar component on the player object and deal damage
            Healthbar playerHealth = other.GetComponent<Healthbar>();
            if (playerHealth != null)
            {
                playerHealth.Heal(damageAmount);  // Deal damage to the player
            }

            // Destroy the object this script is attached to (e.g., the damage zone)
            Destroy(gameObject);
        }
    }
}
