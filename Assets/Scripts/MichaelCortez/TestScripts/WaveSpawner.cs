using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  // Add the SceneManagement namespace

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private TextMeshProUGUI waveText;
    public Wave[] waves;

    public int currentWaveIndex = 0;
    private bool spawningComplete = false;

    [SerializeField] private float delayBeforeNextScene = 5f; // Time to wait before loading the next scene (in seconds)
    [SerializeField] private string nextLevelName = "NextLevel"; // Name of the next scene to load

    private void Start()
    {
        // Setup enemiesLeft counts
        for (int i = 0; i < waves.Length; i++)
            waves[i].enemiesLeft = waves[i].enemies.Length;

        UpdateWaveText();
        StartCoroutine(SpawnWave()); // Start first wave immediately
    }

    private void Update()
    {
        // Stop after last wave
        if (spawningComplete) return;

        // When all enemies in wave are dead -> Next wave
        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            currentWaveIndex++;

            if (currentWaveIndex >= waves.Length)
            {
                spawningComplete = true;
                Debug.Log("All waves completed!");
                waveText.text = "Waves Complete!";

                // Start the delay before loading the next level
                StartCoroutine(WaitAndLoadNextLevel());
                return;
            }

            UpdateWaveText();
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        Debug.Log("Spawning Wave " + (currentWaveIndex + 1));

        Wave wave = waves[currentWaveIndex];

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            GameObject spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

            EnemyAi enemy = Instantiate(
                wave.enemies[i],
                spawn.transform.position,
                Quaternion.identity
            );

            // Set parent only if you want organization
            enemy.transform.SetParent(spawn.transform);

            yield return new WaitForSeconds(wave.timeToNextEnemy);
        }
    }

    private void UpdateWaveText()
    {
        if (waveText != null)
            waveText.text = $"Wave {currentWaveIndex + 1} / {waves.Length}";
    }

    // Coroutine to wait and then load the next level
    private IEnumerator WaitAndLoadNextLevel()
    {
        yield return new WaitForSeconds(delayBeforeNextScene);  // Wait for the specified delay

        // Load the next scene by name
        SceneManager.LoadScene(nextLevelName);
    }

    [System.Serializable]
    public class Wave
    {
        public EnemyAi[] enemies;
        public float timeToNextEnemy;
        [HideInInspector] public int enemiesLeft;
    }
}
