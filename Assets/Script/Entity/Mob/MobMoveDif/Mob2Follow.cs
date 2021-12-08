using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob2Follow : MobMove
{
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void Move()
    {

        if (Detect && IsNotAxe())
        {
            if(CM.IsGrounded())
                CM.Move(Dico.CalculeDirection(target,transform)* 1.5f);
            else
            {
                Detect = false;
                MoveBasic();
            }
        }
        else
        {
            MoveBasic();
        }
    }

    private bool IsNotAxe()
    {
        float posX = target.position.x - transform.position.x;
        if (posX < 2 && posX > -2)
            return false;
        /*
        float posY = target.position.y - transform.position.y;
        if (posY < 5 && posY > -5)
            return true;
        
        return false;
        */

        return true;
    }
}
