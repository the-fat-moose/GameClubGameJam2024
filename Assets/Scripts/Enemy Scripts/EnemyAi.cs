using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject projectile;

    //Patroling
    [SerializeField] private Vector3 walkPoint;
    private bool walkPointSet;
    [SerializeField] private float walkPointRange;
    
    [Header("Gunshot Drawing")]
    [SerializeField] Transform gunTip;
    [SerializeField] Material lineMat;
    private LineRenderer lr;
    [SerializeField] private float timeBetweenAttacks;
    private bool alreadyAttacked;
    RaycastHit hit;
    [SerializeField] private Transform gunPoint;

    [Header("Basic Gun Mechanics")]
    private float damage = 5f;
    [SerializeField] private float range = 100f;

    [Header("States")]
    public float sightRange, attackRange;
    [SerializeField] private bool playerInSightRange, playerInAttackRange;

    private Animator enemyAnimator;

    private GameObject gameEndCollider;
    private Target target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponentInChildren<Animator>();
        target = GetComponent<Target>();
        gameEndCollider = GameObject.Find("GameEndCollider");

        if (player == null) { player = GameObject.FindFirstObjectByType<CageTarget>().transform; }
    }

    public void SetCreatureCage(GameObject _player)
    {
        player = _player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEndCollider != null && gameEndCollider.GetComponent<GameEnd>().gameIsPaused == false)
        {
            if (player != null && target.health > 0f)
            {
                //check for sight and attack range
                playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

                if (!playerInSightRange && !playerInAttackRange)
                {
                    Patroling();
                }
                if (playerInSightRange && !playerInAttackRange)
                {
                    if (player.GetComponent<CageTarget>() != null && !player.GetComponent<CageTarget>().isDead)
                    {
                        ChasePlayer();
                    }
                }
                if (playerInSightRange && playerInAttackRange)
                {
                    if (player.GetComponent<CageTarget>() != null && !player.GetComponent<CageTarget>().isDead)
                    {
                        AttackPlayer();
                    }
                }
            }
        }
    }

    private void Patroling()
    {
        enemyAnimator.SetBool("IsAttackingCage", false);
        enemyAnimator.SetBool("IsChasingCage", false);
        enemyAnimator.SetBool("IsPatrolling", true);

        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate ransom point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        enemyAnimator.SetBool("IsAttackingCage", false);
        enemyAnimator.SetBool("IsChasingCage", true);
        enemyAnimator.SetBool("IsPatrolling", false);

        agent.SetDestination(player.position);
    }

    private void AttackPlayer() 
    {
        enemyAnimator.SetBool("IsAttackingCage", true);
        enemyAnimator.SetBool("IsChasingCage", false);
        enemyAnimator.SetBool("IsPatrolling", true);

        //Make Sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        if(!alreadyAttacked)
        {
            //attack code
            /*Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);*/

            Shoot();
            
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void Shoot()
    {
        gunPoint.transform.LookAt(player.position);
        if (Physics.Raycast(gunPoint.position, gunPoint.transform.forward, out hit, range))
        {
            if (!alreadyAttacked)
            {
                alreadyAttacked = true;
                if (gameObject.GetComponent<LineRenderer>() == null)
                {
                    lr = gameObject.AddComponent<LineRenderer>();
                }
                lr.positionCount = 2;

                DrawShot();

                if (hit.transform.CompareTag("CreatureCage"))
                {
                    if (hit.transform.GetComponent<CageTarget>() != null)
                    {
                        hit.transform.GetComponent<CageTarget>().TakeDamage(damage);
                    }
                   
                }
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    private void DrawShot()
    {
        if (!lr) return;
        lr.material = lineMat;
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, hit.point);
        StartCoroutine(DeleteShotLine());
    }

    private IEnumerator DeleteShotLine()
    {
        yield return new WaitForSeconds(0.1f);

        Destroy(lr);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
