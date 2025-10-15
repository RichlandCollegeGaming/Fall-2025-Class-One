using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Rotation speeds for each axis
    public float rotationSpeedX = 10f;
    public float rotationSpeedY = 10f;
    public float rotationSpeedZ = 10f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object based on the time passed and the speeds for each axis
        float rotationX = rotationSpeedX * Time.deltaTime;
        float rotationY = rotationSpeedY * Time.deltaTime;
        float rotationZ = rotationSpeedZ * Time.deltaTime;

        // Apply rotation
        transform.Rotate(rotationX, rotationY, rotationZ);
    }
}
