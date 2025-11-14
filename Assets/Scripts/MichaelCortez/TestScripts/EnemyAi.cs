using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public WaveSpawner waveSpawner;   // Assigned directly by spawner

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, AttackRange;

    [SerializeField] private Slider _healthbarSlider;

    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public GameObject deathPrefab;
    public GameObject specialPrefab;

    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _healthbarSlider.value = currentHealth;
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f;

        if (directionToPlayer.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    public void UpdateHealthBar(float currentHealth)
    {
        _healthbarSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar(currentHealth);

        if (currentHealth <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        if (deathPrefab != null)
            Instantiate(deathPrefab, transform.position, transform.rotation);

        if (specialPrefab != null && Random.value < 0.33f)
            Instantiate(specialPrefab, transform.position, transform.rotation);

        // Decrease wave count safely
        if (waveSpawner != null)
            waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;

        Destroy(gameObject);
    }
}
