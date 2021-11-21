using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob1 : MobMove
{
    public override ControllerMove CM
    {
        get;
        set;
    }

    public override void Move()
    {

        if (!CM.IsGrounded())
        {
            if (!CM.directionLock)
            {

                CM.direction = -CM.direction;
                CM.directionLock = true;

            }
            CM.Move(CM.direction / 10);
        }
        else
        {
            CM.Move(CM.direction);
            CM.directionLock = false;
        }
    }
}
