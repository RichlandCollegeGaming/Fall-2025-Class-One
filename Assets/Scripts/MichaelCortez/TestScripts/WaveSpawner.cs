using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private TextMeshProUGUI waveText;
    public Wave[] waves;

    public int currentWaveIndex = 0;
    private bool spawningComplete = false;

    [SerializeField] private float delayBeforeNextScene = 3f; // Time to wait before loading the next scene (in seconds)
    [SerializeField] private string nextLevelName = "WinScreen"; // Name of the next scene to load

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

        // Check if all enemies in the current wave are dead
        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            // If it's the final wave, wait 3 seconds before loading the WinScreen
            if (currentWaveIndex >= waves.Length - 1)
            {
                spawningComplete = true;
                waveText.text = "Waves Complete!";
                StartCoroutine(WaitAndLoadNextLevel());
                return;
            }

            // Otherwise, move to the next wave
            currentWaveIndex++;
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

        // Load the next scene by name (WinScreen in this case)
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
