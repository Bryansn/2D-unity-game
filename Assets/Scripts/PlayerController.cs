using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float normalJumpStrength = 17f;
    public float boostedJumpStrength = 200f;
    private float currentJumpStrength;
    private Animator animator;


    private Vector3 starting_position;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        starting_position = transform.position;
        currentJumpStrength = normalJumpStrength;
    }

    private bool grounded = false;

    // Update is called once per frame
    void Update()
    {
        float horiizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horiizontal), 0.1f, Time.deltaTime);


        if (horiizontal > 0)
        {
            desiredx = 6f;
        }
        else if (horiizontal < 0) {
            desiredx = -6f;
        }
        else
        {
            desiredx = 0f;
        }

        //jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = currentJumpStrength;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Debug.Log("Power-up collected!");

            currentJumpStrength = boostedJumpStrength;

            Destroy(other.gameObject);
        }
    }


}
