using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public int maxHealth = 4;
    public int currentHealth;
    public HealthBar healthBar;
    public GameManager applecounter;
    //public AppleCollector appleCollector;

    [Header("Jump Settings")]
    public float normalJumpStrength = 25f;
    public float boostedJumpStrength = 200f;
    public float wallJumpHorizontalStrength = 7f;
    public float wallJumpVerticalStrength = 20f;

    [Header("Timings")]
    public float wallJumpLockDuration = 0.18f; // how long X is locked so FixedUpdate won't override it
    public float jumpGraceDuration = 0.12f;    // how long vertical collisions are ignored after jump

    private float currentJumpStrength;
    private Animator animator;
    private Vector3 starting_position;

    private bool grounded = false;
    private bool wallCheck = false;
    private int wallDir = 0; // -1 = left wall, +1 = right wall

    void Start()
    {   
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        starting_position = transform.position;
        currentJumpStrength = normalJumpStrength;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

        currentJumpStrength = 25f;
        // read input normally (we do NOT early-return, because we still want to detect jump)
        float horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal), 0.1f, Time.deltaTime);

        // set desired x (this will be used by PhysicsObject unless horizontal is locked)
        if (horizontal > 0)
            desiredx = 9f;
        else if (horizontal < 0)
            desiredx = -9f;
        else
            desiredx = 0f;

        // ground jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = currentJumpStrength;
            grounded = false;

            // prevent the vertical from being killed in the same/next Physics frame
            StartJumpGrace(jumpGraceDuration);
        }

        // wall jump: only require wallCheck (direction decided in CollideWithHorizontal)
        if (wallCheck && Input.GetButtonDown("Jump"))
        {
            // launch away from the wall
            velocity = new Vector3(-wallDir * wallJumpHorizontalStrength, wallJumpVerticalStrength, 0f);
            grounded = false;
            wallCheck = false;

            // lock horizontal so FixedUpdate doesn't overwrite velocity.x
            LockHorizontal(wallJumpLockDuration);

            // small vertical grace so Movement doesn't zero velocity.y right away
            StartJumpGrace(jumpGraceDuration);
        }
    }

    public override void CollideWithVertical(Collider2D other)
    {
        base.CollideWithHorizontal(other);
        if (other.gameObject.CompareTag("Water"))
        {
            transform.position = starting_position;
            velocity = Vector3.zero;
            grounded = false;
        }
        else if (other.TryGetComponent<AppleCollector>(out var apple))
        {
            apple.Collect(this);
            applecounter.appleCounter += 1;
        }
        else
        {
            // Note: PhysicsObject's Movement will only call this when jumpGraceTimer <= 0
            grounded = true;
            velocity.y = 0;
        }
    }

    public override void CollideWithHorizontal(Collider2D other)
    {
        // detect side of the wall (left/right) so we know which way to jump
        if (Input.GetAxis("Horizontal") > 0){
            wallDir = 1;
            TakeDamage(50);
        }
        else
        {
            TakeDamage(50);
            wallDir = -1;
        }

        if (!grounded)
        {
            // simple wall slide value
            velocity.y = Mathf.Max(velocity.y, -3f);
        }
        base.CollideWithHorizontal(other);
        if (other.TryGetComponent<AppleCollector>(out var apple))
        { 
            apple.Collect(this);
            applecounter.appleCounter += 1;
        }
        else
        {
            wallCheck = true;
        }


    }

    void TakeDamage( int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("PowerUp"))
    //    {
    //        Debug.Log("Power-up collected!");
    //        currentJumpStrength = boostedJumpStrength;
    //        Destroy(other.gameObject);
    //    }
    //}

}
    //private IEnumerator DisableHorizontalMovement(float duration)
    //{
    //    // not used in the approach above, but kept for reference
    //    float saveDesired = desiredx;
    //    desiredx = 0f;
    //    yield return new WaitForSeconds(duration);
    //    desiredx = saveDesired;
    //}
