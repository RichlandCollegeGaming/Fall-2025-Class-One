using UnityEngine;

public class GameController : MonoBehaviour
{



    public GameObject ball;
    public Transform spawnPoint;

    public float maxX;
    public float maxZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpawnBall();

        InvokeRepeating("SpawnBall", 1f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
        }

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos = Input.mousePosition;

            mousePos.z = 10f;

            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(mousePos);

            Instantiate(ball, spawnPos,Quaternion.identity);    

        }
    }

    void SpawnBall()
    {


        float randomX = Random.Range(-maxX,maxX);
        float randomZ = Random.Range(-maxZ,maxZ);   

        Vector3 randomSpawnPos = new Vector3 (randomX, 10f , randomZ);  
        Instantiate(ball, randomSpawnPos,Quaternion.identity);


    }


}
