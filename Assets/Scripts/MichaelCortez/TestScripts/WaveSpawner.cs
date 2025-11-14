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

    [SerializeField] private float delayBeforeNextScene = 3f;
    [SerializeField] private string nextLevelName = "WinScreen";

    private void Start()
    {
        // Initialize enemiesLeft for each wave
        for (int i = 0; i < waves.Length; i++)
            waves[i].enemiesLeft = waves[i].enemies.Length;

        UpdateWaveText();
        StartCoroutine(SpawnWave());
    }

    private void Update()
    {
        if (spawningComplete) return;

        // If all enemies of current wave are dead
        if (waves[currentWaveIndex].enemiesLeft <= 0)
        {
            // FINAL WAVE COMPLETED Load next scene
            if (currentWaveIndex == waves.Length - 1)
            {
                spawningComplete = true;
                waveText.text = "Waves Complete!";
                StartCoroutine(WaitAndLoadNextLevel());
                return;
            }

            // Move to next wave
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

            // Give the enemy a reference to THIS WaveSpawner
            enemy.waveSpawner = this;

            yield return new WaitForSeconds(wave.timeToNextEnemy);
        }
    }

    private void UpdateWaveText()
    {
        if (waveText != null)
            waveText.text = $"Wave {currentWaveIndex + 1} / {waves.Length}";
    }

    private IEnumerator WaitAndLoadNextLevel()
    {
        yield return new WaitForSeconds(delayBeforeNextScene);
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
