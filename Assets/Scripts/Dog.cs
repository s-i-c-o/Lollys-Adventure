using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dog : MonoBehaviour
{
    private GameObject player;
    private Vector2 target;
    private Vector2 toTarget;
    public float speed = 10.0f;
    public float heightAdjuster = 0.6f;
    public float widthAdjuster = 1.75f;
    private bool findingButton;
    // UI
    private Button dogButton;
    private Animator animator;
    private bool movingRight;
    private AudioSource bark;

    private BoxCollider2D bc;
    private CircleCollider2D cc;

    public bool bIsAttacking { get; set; }
    private int enemiesInRange;
    private GameObject targetEnemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bc = GetComponent<BoxCollider2D>();
        cc = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        bark = GetComponent<AudioSource>();
        bIsAttacking = false;
        findingButton = true;
        movingRight = true;
        enemiesInRange = 0;  
    }

    private void AssignButton()
    {
        Debug.Log("Assinging button");
        dogButton = GameObject.FindGameObjectWithTag("DogButton").GetComponent<Button>();
        dogButton.onClick.AddListener(AttackEnemy);
        findingButton = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (findingButton)
        {
            Debug.Log("checking");
            if (GameObject.FindGameObjectWithTag("DogButton") != null)
            {
                AssignButton();
            }
        }

        if (!bIsAttacking)
        {
            FollowPlayer();
        }
        else
        {
            AttackEnemy();
        }

        if (toTarget.x > 0)
        {
            movingRight = true;
            animator.SetBool("bMovingRight", true);
        }
        else if (toTarget.x < 0)
        {
            movingRight = false;
            animator.SetBool("bMovingRight", false);
        }
        else if (toTarget.x == 0)
        {
            animator.SetBool("bMovingRight", movingRight);
        }

        if (PlayerStats.Instance.bWalkingDirection)
        {
            widthAdjuster = 1.75f;
        }
        else
        {
            widthAdjuster = -1.75f;
        }
    }

    void FollowPlayer()
    {
        // Normal Positioning
        target = player.transform.position;

        toTarget.x = target.x - transform.position.x;
        toTarget.y = target.y - transform.position.y;

        target.y = target.y - heightAdjuster;
        target.x = target.x - widthAdjuster;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void AttackEnemy()
    {
        Debug.Log("Enemies in range: " + enemiesInRange);
        if (enemiesInRange > 0)
        {
            bIsAttacking = true;
            if (targetEnemy != null)
            {
                toTarget.x = targetEnemy.transform.position.x - transform.position.x;
                toTarget.y = targetEnemy.transform.position.y - transform.position.y;
                transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
            }
            else
            {
                bIsAttacking = false;
            }

            // Bork
            bark.Play();
        }
    }

    public void FinishAttack()
    {
        bIsAttacking = false;
        enemiesInRange--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && cc.IsTouching(collision))
        {
            enemiesInRange++;
            targetEnemy = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemiesInRange--;
        }
    }
}
