using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyDamage : MonoBehaviour
{
    public float damage = 10f;
    public float attackRate = 1f; // seconds between attacks
    private float nextAttackTime = 0f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                //player.take.damage(damage);
                nextAttackTime = Time.time + attackRate;
            }
        }
    }
}

