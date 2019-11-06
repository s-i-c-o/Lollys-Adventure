using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform : MonoBehaviour
{
    public Vector2 firstPatrolPoint;
    public Vector2 secondPatrolPoint;
    public float movementSpeedModifier = 2.0f;
    public float minGroundNormalY = 0.65f;

    private Vector2 movementTarget;
    private int targetNum;

    protected Rigidbody2D rb;
    protected Vector2 targetVelocity;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;
    protected Vector2 groundNormal;

    private bool bIsTargetingPlayer;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bIsTargetingPlayer = false;
        targetNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    private void FixedUpdate()
    {
        velocity.x = targetVelocity.x;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Debug.Log("move before: " + moveAlongGround.x + ", " + moveAlongGround.y);
        Vector2 move = new Vector2(1, 0) * deltaPosition.x;
        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        Debug.Log("Move: " + move.x + ", " + move.y);
        float distance = move.magnitude;

        rb.position = rb.position + move.normalized * distance;
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
                        Debug.Log("Should move left");
                        targetVelocity = Vector2.left * movementSpeedModifier;
                    }
                    else if (dir.x > 0)
                    {
                        // Pointed right
                        Debug.Log("Should move right");
                        targetVelocity = Vector2.right * movementSpeedModifier;
                    }
                }
                else
                {
                    // We are at target
                    SwapTarget();
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
}
