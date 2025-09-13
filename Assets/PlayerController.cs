using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    private Vector3 starting_position;

    // Start is called before the first frame update
    void Start()
    {
        starting_position = transform.position;    
    }

    private bool grounded = false;

    // Update is called once per frame
    void Update()
    {
        float horiizontal = Input.GetAxis("Horizontal");

        if (horiizontal > 0)
        {
            desiredx = 3f;
        }
        else if (horiizontal < 0) {
            desiredx = -3f;
        }
        else
        {
            desiredx = 0f;
        }

        //jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            //velocity.y = 6.5f;
            velocity.y = 10f;
            grounded = false;
        }
    }

    public override void CollideWithVertical(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            transform.position = starting_position;
            velocity = Vector3.zero;
            grounded = false;
        }
        else
        {
            grounded = true;
            velocity.y = 0;
        }
            //grounded = true;
    }
}
