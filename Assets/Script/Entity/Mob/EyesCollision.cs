using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesCollision : Eyes
{
    public override void FixedUpdate()
    {
        if (Physics2D.OverlapBox(transform.position,new Vector2(1,1),90, mob.controllerMove.whatisGround))
        {
            if (mob.Detect)
                mob.Detect = false;
            mob.Flip();
        }
    }
}
