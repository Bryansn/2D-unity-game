using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // Public velocity so we can see/change it in the Inspector 
    public Vector3 velocity = Vector3.zero;
    public float desiredx;

    // Timers for locking / grace
    // When horizontalLockTimer > 0, FixedUpdate will NOT overwrite velocity.x with desiredx.
    // When jumpGraceTimer > 0, Movement will NOT zero velocity.y on vertical collisions.
    public float horizontalLockTimer = 0f;
    public float jumpGraceTimer = 0f;

    // --- Utility methods other scripts (like PlayerController) can call ---
    public void LockHorizontal(float duration)
    {
        horizontalLockTimer = duration;
    }

    public void StartJumpGrace(float duration)
    {
        jumpGraceTimer = duration;
    }

    // Start is empty in your original
    void Start()
    {
    }

    // FixedUpdate runs physics steps
    void FixedUpdate()
    {
        // decrement timers
        if (horizontalLockTimer > 0f) horizontalLockTimer -= Time.fixedDeltaTime;
        if (jumpGraceTimer > 0f) jumpGraceTimer -= Time.fixedDeltaTime;

        // Gravity acceleration
        Vector3 acceleration = -9.81f * Vector3.up * 4f;

        // Update velocity
        velocity += acceleration * Time.fixedDeltaTime;

        // Update horizontal velocity only if not locked
        if (horizontalLockTimer <= 0f)
        {
            velocity.x = desiredx;
        }
        // else: keep whatever velocity.x was set by e.g. a wall-jump

        // Split movement into x and y components
        Vector2 movement = velocity * Time.fixedDeltaTime;
        Movement(new Vector2(movement.x, 0), true);
        Movement(new Vector2(0, movement.y), false);
    }

    void Movement(Vector2 move, bool movex = true)
    {
        if (move.magnitude < 0.00001f) return;

        RaycastHit2D[] results = new RaycastHit2D[16];
        int cnt = GetComponent<Rigidbody2D>().Cast(move, results, move.magnitude + 0.01f);

        for (int i = 0; i < cnt; i++)
        {
            Vector2 normal = results[i].normal;
            if (movex)
            {
                if (Mathf.Abs(normal.x) > 0.5f)
                {
                    // If we're locked horizontally (because of a wall-jump), skip cancelling X movement
                    if (horizontalLockTimer <= 0f)
                    {
                        move.x = 0;
                        velocity.x = 0;
                        CollideWithHorizontal(results[i].collider);
                        transform.position += (Vector3)(normal * 0.01f);
                    }
                    else
                    {
                        CollideWithHorizontal(results[i].collider);
                        // horizontal is locked: ignore the horizontal collision for this frame so we can move away
                        // (do not zero velocity.x and do not call the collision callback)
                    }
                }
            }
            else
            {
                if (Mathf.Abs(normal.y) > 0.5f)
                {
                    // If we're in jump grace, ignore the vertical cancellation for a short time
                    if (jumpGraceTimer <= 0f)
                    {
                        move.y = 0;
                        velocity.y = 0;
                        CollideWithVertical(results[i].collider);
                        transform.position += (Vector3)(normal * 0.01f);
                    }
                    else
                    {
                        CollideWithVertical(results[i].collider);
                        // in jump grace: ignore vertical collision that would kill upward velocity
                    }
                }
            }
        }

        transform.position += (Vector3)(move);
    }

    public virtual void CollideWithHorizontal(Collider2D other) { }

    public virtual void CollideWithVertical(Collider2D other) { }

    //public virtual void OnTriggerEnter2D(Collider2D other) { }
}
