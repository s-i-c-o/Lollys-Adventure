using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    public int lives = 5;

    public float bounceModifier = 7.0f;
    protected Vector2 bounceDir;

    protected Vector2 lastCheckpointLoc;

    protected Animator animator;
    protected bool walkingDirection; // True is right, false is left

    private AudioSource boop;

    // Start is called before the first frame update
    void Start()
    {
        lastCheckpointLoc = rb.position;

        animator = GetComponent<Animator>();
        boop = GetComponent<AudioSource>();
        if (boop != null)
        {
            Debug.Log("got boop");
        }
        walkingDirection = true;
    }


    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        //move.x = CrossPlatformInputManager.GetAxis("Horizontal");
        move.x = SimpleInput.GetAxis("Horizontal");

        if (move.x > 0)
        {
            walkingDirection = true;
            animator.SetBool("bWalkingRight", true);
        }
        else if (move.x < 0)
        {
            walkingDirection = false;
            animator.SetBool("bWalkingRight", false);
        }
        else if (move.x == 0)
        {
            animator.SetBool("bWalkingRight", walkingDirection);
        }

        if(SimpleInput.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (SimpleInput.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

        targetVelocity = move * maxSpeed;

        PlayerStats.Instance.bWalkingDirection = walkingDirection;
    }

    public void TossPlayer(Vector2 dir)
    {
        Vector2 launchVec = new Vector2(-jumpTakeOffSpeed, jumpTakeOffSpeed);
        velocity = launchVec;

    }

    public void BouncePlayer()
    {
        velocity.y = jumpTakeOffSpeed * 0.4f;
        boop.Play();
    }

    public void CloudBouncePlayer()
    {
        velocity.y = jumpTakeOffSpeed * 1.3f;
        boop.Play();
    }

    public int GetLives()
    {
        return lives;
    }

    public void SetLives(int num)
    {
        lives = num;
    }

    public void LoseLifeAndReset()
    {
        boop.Play();
        lives--;
        Debug.Log("Lives now at: " + lives);
        if (lives > 0)
        {
            ResetPositionToLastCheckpoint();
        }
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log("Lives now at: " + lives);
    }

    public Vector2 GetLastCheckpointLocation()
    {
        return lastCheckpointLoc;
    }

    public void SetLastCheckpointLocation(Vector2 loc)
    {
        lastCheckpointLoc = loc;
    }

    protected void ResetPositionToLastCheckpoint()
    {
        velocity = Vector2.zero;
        rb.position = lastCheckpointLoc;
    }
}
