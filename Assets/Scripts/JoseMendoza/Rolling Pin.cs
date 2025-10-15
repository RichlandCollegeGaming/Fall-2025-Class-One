using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    private object input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AssignInputs()
    {
      //  input.Attack.started += ctx => Attack();
    }

    // ------------------ //
    // ATTACKING BEHAVIOR //
    // ------------------ //

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public float attackDamage = 1f;
    public LayerMask attackLayer;
}
