using System;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class WeaponSwitching : MonoBehaviour
{
    public int selectWeapon = 0;
    private int previousSelectedWeapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            int previousSelectedWeapon = selectWeapon;
            
            if (selectWeapon >= transform.childCount - 1)
            {
                selectWeapon = 0;
                selectWeapon = 0;
            }
            else
            {
                selectWeapon++;
            }

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectWeapon <= 0)
            {
                selectWeapon = transform.childCount - 1;
                selectWeapon = 0;
            }

            else
            {
                selectWeapon--;
            }

            if (previousSelectedWeapon != selectWeapon)
            {
                SelectedWeapon();             
            }
        }
    }

    private void SelectedWeapon()
    {
        throw new NotImplementedException();
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
