using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesCollision : Eyes
{
    public override void FixedUpdate()
    {
        if (Physics2D.OverlapBox(transform.position+ new Vector3(0,1),new Vector2(0.2f, (float)(mob.controllerMove.feetPos.bounds.size.y*0.6)),0, mob.controllerMove.whatisGround))
        {
            if (mob.Detect)
                mob.Detect = false;
            mob.Flip();
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 1), new Vector2(1, mob.controllerMove.feetPos.bounds.size.y));
    }
}
