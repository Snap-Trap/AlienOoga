using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BobAi : MonoBehaviour
{
    // Makes a reference to the NavMeshAgent component
    public NavMeshAgent agent;

    // Player location
    public Transform player;

    // So it can tell the difference between ground and player, also neat to dodge random obstacles
    public LayerMask whatIsGround, whatIsPlayer, whatIsWall;

    // Basic variables

    public int health;

    // This is patroling ai

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // This is attacking ai
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    // To switch between ai states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, playerInLineSight;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Checks for the sight range and attack range in a sphere around the enemy
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        //Vector3 playerDirection = player.position - transform.position;

        // Alright let's just do this with a simple fucking spherecast because I am about to lose my mind

        if (playerInSightRange)
        {

        }
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Simple logic, it will either patrol, chase, or attack the player depending on what states the variables are in
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        // If the enemy doesn't have a walk point set, it will search for one
        if (!walkPointSet) SearchWalkPoint();

        // NavMesh W moment
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        // This is how much space or distance or whatever there is between the enemy and the walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // If the enemy gets close to the walk point it will say it doesn't have a walk point so it can search for a new one
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // If the walk point is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        // Th
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Enemy will set in place once the attack range is plenty so it doesn't push into the player
        agent.SetDestination(transform.position);

        // Rotates towards the player
        transform.LookAt(player);
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }



    //public void TakeDamge(int damage)
    //{
    //    health -= damage;

    //    if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    //}

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Bullet"))
    //    {
    //        TakeDamge(1);
    //    }
    //}

    //private void DestroyEnemy()
    //{
    //    Destroy(gameObject);
    //}



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}