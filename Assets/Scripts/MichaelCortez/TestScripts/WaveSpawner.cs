using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] private float countdown;

    [SerializeField] private GameObject SpawnPoint;

    public Wave[] waves;


    private int currentWaveIndex = 0;


    private void Update()
    {
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


        for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
        {

            Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], SpawnPoint.transform);

            enemy.transform.SetParent(SpawnPoint.transform);

            yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
        }




    }





    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;

        public float timeToNextEnemy;
        public float timeToNextWave;

        [HideInInspector] public int enemiesLeft;
    }




    private void Start()
    {
        for (int i = 0;i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length; 
        }
    }

}