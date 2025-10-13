using System;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectWeapon(); // Initialize the weapon selection
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        // Mouse Scroll Up
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            selectedWeapon++;
            if (selectedWeapon >= transform.childCount) selectedWeapon = 0; // Wrap around if we exceed the number of weapons
        }

        // Mouse Scroll Down
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selectedWeapon--;
            if (selectedWeapon < 0) selectedWeapon = transform.childCount - 1; // Wrap around if we go below zero
        }

        // Number keys to directly select weapons
        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount > 0) selectedWeapon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount > 1) selectedWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount > 2) selectedWeapon = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount > 3) selectedWeapon = 3;

        // If the weapon has changed, update it
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    // Method to select the active weapon based on the selectedWeapon index
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true); // Activate the selected weapon
            else
                weapon.gameObject.SetActive(false); // Deactivate all other weapons
            i++;
        }
    }
}
