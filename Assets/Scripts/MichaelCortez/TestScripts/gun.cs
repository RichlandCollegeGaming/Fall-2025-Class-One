using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Bullet Settings")]
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    [Header("Firing Settings")]
    public int maxShots = 5;           // Shots before cooldown
    public float cooldownTime = 3f;    // Cooldown duration in seconds

    private int shotsFired = 0;
    private bool isCooldown = false;
    private float cooldownEndTime = 0f;

    private void Update()
    {
        // If we're on cooldown, check if the time has passed
        if (isCooldown && Time.time >= cooldownEndTime)
        {
            EndCooldown();
        }

        // Fire if mouse is pressed, not on cooldown
        if (Input.GetMouseButtonDown(0) && !isCooldown)
        {
            Fire();
        }
    }

    private void Fire()
    {
        // Instantiate and shoot the bullet
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

        shotsFired++;

        // Start cooldown if shot limit reached
        if (shotsFired >= maxShots)
        {
            StartCooldown();
        }
    }

    private void StartCooldown()
    {
        isCooldown = true;
        cooldownEndTime = Time.time + cooldownTime;
        Debug.Log("Cooldown started...");
    }

    private void EndCooldown()
    {
        isCooldown = false;
        shotsFired = 0;
        Debug.Log("Cooldown finished! You can fire again.");
    }

    private void OnDisable()
    {
        // Cooldown continues even if disabled — no need to stop or pause it
        // When re-enabled, Update() will check if cooldown expired
    }
}
