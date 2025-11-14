using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10; // Amount of damage to deal
    [SerializeField] private float damageInterval = 2f; // Time interval to wait before dealing damage again (in seconds)

    private bool playerInZone = false;  // To check if the player is inside the trigger zone
    private bool canDamage = true; // To prevent continuous damage application until the cooldown is over

    // Reference to the Healthbar script on the player
    private Healthbar playerHealth;

    void Start()
    {
        // Initialize the playerHealth reference to null
        playerHealth = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // When the player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            // Get the Healthbar component attached to the player
            playerHealth = other.GetComponent<Healthbar>();

            // If the player has a Healthbar component, start the damage process
            if (playerHealth != null)
            {
                playerInZone = true;
                if (canDamage) // Check if we can apply damage
                {
                    StartCoroutine(DealDamageAfterInterval()); // Start the damage process immediately
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When the player exits the trigger zone, stop applying damage
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            playerHealth = null; // Reset reference to stop damage
        }
    }

    private IEnumerator DealDamageAfterInterval()
    {
        // Deal damage first when the player enters the trigger
        if (playerHealth != null)
        {
            playerHealth.DealDamage(damageAmount); // Deal damage immediately
        }

        // Now start the cooldown before applying damage again
        canDamage = false;

        // Wait for the set damage interval (e.g., 2 seconds)
        yield return new WaitForSeconds(damageInterval);

        // After cooldown, allow damage again
        canDamage = true;

        // If the player is still in the zone, start the damage process again
        if (playerInZone && playerHealth != null)
        {
            StartCoroutine(DealDamageAfterInterval()); // Recurse to continue dealing damage after each interval
        }
    }
}
