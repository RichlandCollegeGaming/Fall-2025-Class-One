using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGroud, whatIsPlayer;

    private WaveSpawner waveSpawner;

    private float countdown = 5f;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, AttackRange;
    public bool playerInSightRange, playerInAttackRange;

    [SerializeField] private Slider _healthbarSlider; // Reference to the slider UI component
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [Header("Death Effect")]
    public GameObject deathPrefab; // Prefab to spawn when enemy dies

    public GameObject specialPrefab; // The secondary prefab to spawn with 1/3 chance

    private void Start()
    {
        waveSpawner = GetComponentInParent<WaveSpawner>();
        _healthbarSlider.value = currentHealth;
    }

    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Always chase the player, regardless of sight range or attack range
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        // Move towards the player continuously
        agent.SetDestination(player.position);

        // Calculate the direction to the player
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f; // Keep only the Y-axis unaffected

        // Rotate the enemy to face the player on the Y-axis only
        if (directionToPlayer.sqrMagnitude > 0.01f) // Avoids a small flicker when the enemy is already facing the player
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Smooth rotation
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void UpdateHealthBar(float currentHealth)
    {
        _healthbarSlider.value = currentHealth; // Directly set the slider value to current health
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        UpdateHealthBar(currentHealth);

        if (currentHealth < 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }


    private void DestroyEnemy()
    {
        // Spawn the first prefab (deathPrefab)
        if (deathPrefab != null)
        {
            Instantiate(deathPrefab, transform.position, transform.rotation);
        }

        // 1/3 chance to spawn the secondary prefab (specialPrefab)
        if (specialPrefab != null && Random.value < 0.33f)  // 1/3 chance
        {
            Instantiate(specialPrefab, transform.position, transform.rotation);
        }

        waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
        Destroy(gameObject);
    }
}
