using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;

    public GameObject bulletPrefab;

    public float bulletSpeed = 10;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.positition, bulletSpawnPoint);
        }

        bulletSpawnPoint.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;


    }



}
