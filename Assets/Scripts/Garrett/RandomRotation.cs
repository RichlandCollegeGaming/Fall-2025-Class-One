using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private Vector3 rotationSpeed;

    void Start()
    {
        // Generate a random rotation for each axis
        float randomX = Random.Range(0f, 360f);
        float randomY = Random.Range(0f, 360f);
        float randomZ = Random.Range(0f, 360f);

        // Apply the random initial rotation
        transform.rotation = Quaternion.Euler(randomX, randomY, randomZ);

        // Set a random rotation speed for continuous rotation
        rotationSpeed = new Vector3(
            Random.Range(10f, 50f),
            Random.Range(10f, 50f),
            Random.Range(10f, 50f)
        );
    }

    void Update()
    {
        // Continuously rotate the object based on the random speed
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
