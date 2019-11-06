using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemy : PhysicsObject
{
    public Vector2 firstPatrolPoint;
    public Vector2 secondPatrolPoint;
    public float movementSpeedModifier = 2.0f;

    private Vector2 movementTarget;
    private int targetNum;
    private CapsuleCollider2D bc;

    private bool bIsTargetingPlayer;
    private bool bMovingTowardsWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<CapsuleCollider2D>();
        bIsTargetingPlayer = false;
        bMovingTowardsWaypoint = false;
        targetNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        if (bIsTargetingPlayer)
        {
            // Go towards player
        }
        else
        {
            // patrol
            if (targetNum == 0)
            {
                // We don't have a target yet so set some stuff up

                // Set our y's
                firstPatrolPoint.y = rb.position.y;
                secondPatrolPoint.y = rb.position.y;
                // If we aren't moving yet, find where to go
                float firstDistance = Vector2.Distance(rb.position, firstPatrolPoint);
                float secondDistance = Vector2.Distance(rb.position, secondPatrolPoint);

                if (firstDistance > secondDistance)
                {
                    movementTarget = secondPatrolPoint;
                    targetNum = 2;
                }
                else if (firstDistance < secondDistance)
                {
                    movementTarget = firstPatrolPoint;
                    targetNum = 1;
                }
                else
                {
                    movementTarget = firstPatrolPoint;
                    targetNum = 1;
                }
                bMovingTowardsWaypoint = true;
            }
            else
            {
                // Check to see if we're at the point
                float distToTarget = Vector2.Distance(rb.position, movementTarget);
                if (distToTarget > 0.3f)
                {
                    Vector2 dir = movementTarget - rb.position;
                    if (dir.x < 0)
                    {
                        // Pointed left
                        targetVelocity = Vector2.left * movementSpeedModifier;
                    }
                    else if (dir.x > 0)
                    {
                        // Pointed right
                        targetVelocity = Vector2.right * movementSpeedModifier;
                    }
                }
                else
                {
                    // We are at target
                    SwapTarget();
                    bMovingTowardsWaypoint = false;
                }
            }
        }
    }

    private void SwapTarget()
    {
        if (targetNum == 1)
        {
            movementTarget = secondPatrolPoint;
            targetNum = 2;
        }
        else if (targetNum == 2)
        {
            movementTarget = firstPatrolPoint;
            targetNum = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject dog = collision.gameObject;
        if (collision.gameObject.tag == "Dog" && bc.IsTouching(dog.GetComponent<BoxCollider2D>()))
        {
            Dog ricky = dog.GetComponent<Dog>();
            ricky.FinishAttack();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 vecToPlayer = new Vector2();
            vecToPlayer.x = rb.position.x - player.transform.position.x;
            vecToPlayer.y = rb.position.y - player.transform.position.y;
            float collisionAngle = Vector2.Angle(transform.up, vecToPlayer);

            player.GetComponent<PlayerPlatformerController>().TossPlayer(vecToPlayer);
            player.GetComponent<PlayerPlatformerController>().LoseLife();
        }
    }
}
