using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float life = 3;


    [Header("Damage Settings")]
    public int damageAmount = 10; // Amount of damage to deal

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has the "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            // Try to get the EnemyAI component
            EnemyAi enemy = other.GetComponent<EnemyAi>();

            // If found, call TakeDamage
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogWarning($"{other.name} is tagged 'Enemy' but has no EnemyAI component.");
            }
        }
    }

    private void Awake()
    {
        Destroy(gameObject, life);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

