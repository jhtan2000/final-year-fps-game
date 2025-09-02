using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //public Rigidbody rb;
    //public float moveSpeed;
    public Animator anim;

    private bool chasing;
    public float distanceToChase = 10f, distanceToLost = 15f, distanceToStop = 2f;

    private Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;

    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // store starting position of the enemy
        startPoint = transform.position;

        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y; 

        if (!chasing) // idle
        {
            if(Vector3.Distance(transform.position, targetPoint) < distanceToChase) // within range to chase
            {
                chasing = true; // chasing the player

                // reseting time
                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
            }

            if(chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime; // countdown

                if(chaseCounter <= 0)
                {
                    anim.SetBool("isMoving", true);
                    agent.destination = startPoint;

                    if (transform.position == startPoint)
                    {
                        anim.SetBool("isMoving", false);
                    }
                }
            }
        }
        else // chasing
        {

            if(Vector3.Distance(transform.position, targetPoint) > distanceToStop) // social distance
            {
                agent.destination = targetPoint; // make AI chase player
            }
            else
            {
                agent.destination = transform.position; // stop at current position
            }

            if(Vector3.Distance(transform.position, targetPoint) > distanceToLost)
            {
                chasing = false;
                agent.destination = transform.position; // stop at current position
                chaseCounter = keepChasingTime;
            }

            // limiter or timer for waiting between shooting
            if(shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime; // counting down 2 1 0

                if (shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
                anim.SetBool("isMoving", true);
            }
            else if (PlayerController.instance.gameObject.activeInHierarchy) // after 2 seconds and the player alive
            {
                shootTimeCounter -= Time.deltaTime;

                if(shootTimeCounter > 0) // during 1 second it will shoot us
                {
                    fireCount -= Time.deltaTime;

                    if (fireCount <= 0)
                    {
                        fireCount = fireRate;
                        firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f));

                        // check angle to player
                        Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
                        float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

                        if (Mathf.Abs(angle) < 30f) // && player.activeInHierarchy == true)
                        {
                            Instantiate(bullet, firePoint.position, firePoint.rotation);
                            anim.SetTrigger("fireShot");
                        }
                        else
                        {
                            shotWaitCounter = waitBetweenShots;
                        }
                    }

                    agent.destination = transform.position; //stop at his current position
                }
                else
                {
                    shotWaitCounter = waitBetweenShots;
                }
                anim.SetBool("isMoving", false);
            }            
        }        
    }
}
