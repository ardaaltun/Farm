using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatisGround, whatisPlayer;
    public Animator anim;
    public AudioSource audio;
    public AudioClip walk;
    public AudioClip chase;
    public AudioClip attack;
    public AudioClip death;

    bool walking = false;
    bool chasing = false;
    bool attacking = false;
    bool died = false;
    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        if (!playerInSightRange && !playerInAttackRange && !died) Patrol();
        if (playerInSightRange && !playerInAttackRange && !died) Chase();
        if (playerInSightRange && playerInAttackRange && !died) Attack();
        //anim.SetBool("inRange", playerInSightRange);
    }
    void Patrol()
    {
        walking = true;
        chasing = false;
        attacking = false;
        died = false;
        audio.volume = 0.4f;
        if (walking && !audio.isPlaying) audio.PlayOneShot(walk);
        anim.SetBool("inRange", false);
        anim.SetBool("canAttack", false);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround))
            walkPointSet = true;
    }
    void Chase()
    {
        walking = false;
        chasing = true;
        attacking = false;
        died = false;
        audio.volume = 0.6f;
        if (chasing && !audio.isPlaying) audio.PlayOneShot(chase);
        anim.SetBool("inRange", true);
        anim.SetBool("canAttack", false);
        agent.SetDestination(player.position);
    }
    void Attack()
    {
        walking = false;
        chasing = false;
        attacking = true;
        died = false;
        audio.volume = 0.8f;
        if (attacking && !audio.isPlaying) audio.PlayOneShot(attack);
        anim.SetBool("inRange", true);
        anim.SetBool("canAttack", true);
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //attack codes
            print("attacked");
            GameObject.Find("ui settings").GetComponent<uicodes>().health -= 2;
            //attack codes
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
            
    }
    public void DestroyMe()
    {
        walking = false;
        chasing = false;
        attacking = false;
        died = true;
        audio.volume = 0.6f;
        audio.Stop();
        audio.PlayOneShot(death);
        anim.SetBool("inRange", false);
        anim.SetBool("canAttack", false);
        agent.isStopped = true;
        anim.SetBool("death", true);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(gameObject, 2f);

    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
