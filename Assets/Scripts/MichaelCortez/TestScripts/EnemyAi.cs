using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{


    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;

   private bool walkPointset;

    public float walkPointRange;

    //Attacking
    public float timeBetweeenAttack;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;




    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent= GetComponent<NavMeshAgent>();   

    }

    private void Update ()
    {
          //Check for Sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();

        if (playerInAttackRange && playerInSightRange) AttackPlayer();

    }



    private void Patroling()
    {
        if (!walkPointset) SearchWalkPoint();

        if(walkPointset)
            agent.SetDestination(walkPoint);    


        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointset = false;
    }

    private void SearchWalkPoint()
    {
       //Calculate Random point in range

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
     
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomZ, transform.position.z);


        if (Physics.Raycast (walkPoint, -transform.up, 2f, whatIsGround))

          walkPointset = true;  


    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);


        if (!alreadyAttacked)
        {
            alreadyAttacked = true;

         //   Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }



    }

    private void ResetAttack()
    {

        alreadyAttacked = false;   


    }













    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
