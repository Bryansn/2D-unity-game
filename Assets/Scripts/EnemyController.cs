    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject
{
    // Start is called before the first frame update
    void Start()
    {
        desiredx = 3f;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // when hitting something horizontally, reverse direction
    public override void CollideWithHorizontal(Collider2D other)
    {
        //base.CollideWithHorizontal(other);
        desiredx = -desiredx;
    }
}
