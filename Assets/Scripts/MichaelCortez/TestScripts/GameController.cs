using UnityEngine;

public class GameController : MonoBehaviour
{



    public GameObject ball;
    public Transform spawnPoint;
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
        Instantiate(ball, spawnPoint.position,Quaternion.identity);
    }


}
