using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // Public velocity so we can see/change it in the Inspector 
    public Vector3 velocity = Vector3.zero;

    public float desiredx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Gravity acceleration
        Vector3 acceleration = -9.81f * Vector3.up;

        // Update velocity
        velocity += acceleration * Time.fixedDeltaTime;

        // Update Position
        //transform.position += velocity * Time.deltaTime;

        // Call our movement method instead of changing position directly
        velocity.x = desiredx;
        //Movement(velocity * Time.deltaTime);

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
        //if (cnt > 0)
        //{
        //    return;
        //}

        for(int i = 0; i < cnt; i++)
        {
            Vector2 normal = results[i].normal;
            if (movex)
            {
                if (Mathf.Abs(normal.x) > 0.5f)
                {
                    move.x = 0;
                    velocity.x = 0;
                    CollideWithHorizontal(results[i].collider);
                }
            } else
            {
                if (Mathf.Abs(normal.y) > 0.5f)
                {
                    move.y = 0;
                    velocity.y = 0;
                    CollideWithVertical(results[i].collider);
                }
            }
        }

        transform.position += (Vector3)(move);
    }

    public virtual void CollideWithHorizontal(Collider2D other) { }

    public virtual void CollideWithVertical(Collider2D other)
    {
    }
}


