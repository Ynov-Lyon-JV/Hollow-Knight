using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesCollision : Eyes
{
    public override void FixedUpdate()
    {
        if (Physics2D.OverlapPoint(transform.position, mob.controllerMove.whatisGround))
        {
            if (mob.Detect)
                mob.Detect = false;
            mob.Flip();
        }
    }
}
