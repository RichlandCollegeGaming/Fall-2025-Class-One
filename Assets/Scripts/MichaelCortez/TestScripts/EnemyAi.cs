using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   

public class EnemyAi : MonoBehaviour
{
    
    public NavMeshAgent agent; 

    public Transform player;

    public LayerMask whatIsGroud, whatIsPlayer;

    //Patroling
        public Vector3 walkPoint;
    bool walkPointSet;  
    public float walkPointRange;    

    //Attacking
        public float timeBetweenAttacks;
        bool alreadyAttacked;   

    //States
            public float sightRange, AttackRange;
        public bool playerInSightRange, playerInAttackRange;        



    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }   


    private void Patroling()
    {
        //Check for sight and attack range  
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

    }

    private void ChasePlayer()
    {

    }

    private void AttackPlayer()
    {

    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
