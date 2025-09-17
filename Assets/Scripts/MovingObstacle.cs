using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 3f;

    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Move the object
        transform.Translate(Vector2.right * moveSpeed * direction * Time.deltaTime);

        // Reverse direction if we've moved far enough
        if (Vector2.Distance(startPos, transform.position) >= moveDistance)
        {
            direction *= -1;
        }
    }
}
