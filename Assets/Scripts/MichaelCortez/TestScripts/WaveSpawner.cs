using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float countdown;
    [SerializeField] private GameObject SpawnPoint;
    public Wave[] waves;

    public int currentWaveIndex = 0;
    private bool spawningComplete = false;

    private void Start()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
    }

    private void Update()
    {
        if (spawningComplete) return;

        if (currentWaveIndex >= waves.Length)
        {
            spawningComplete = true;
            Debug.Log("All waves completed!");
            return;
        }

        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            countdown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
        }

        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            currentWaveIndex++;
        }
    }

    private IEnumerator SpawnWave()
    {
        if (currentWaveIndex >= waves.Length)
            yield break;

        Debug.Log("Spawning Wave " + (currentWaveIndex + 1) + " of " + waves.Length);

        for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
        {
            EnemyAi enemy = Instantiate(waves[currentWaveIndex].enemies[i], SpawnPoint.transform.position, Quaternion.identity);
            enemy.transform.SetParent(SpawnPoint.transform);

            yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
        }
    }

    [System.Serializable]
    public class Wave
    {
        public EnemyAi[] enemies;
        public float timeToNextEnemy;
        public float timeToNextWave;
        [HideInInspector] public int enemiesLeft;
    }
}
