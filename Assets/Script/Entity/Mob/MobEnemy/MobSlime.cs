using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSlime : Mob
{
    private void Awake()
    {
        isJump = true;
    }

    public override void Move()
    {
        controllerMove.Move(controllerMove.direction / controllerMove.slowTempo, isJump);
    }


    public override void EffectJump()
    {
        GetComponent<ControllerAnimation>().AnimationPlay("Slime");
    }



    public override IEnumerator EffectInvulnerability()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.2f);
            controllerHealth.renderer.color = Color.green;
            yield return new WaitForSeconds(0.2f);
            controllerHealth.renderer.color = Color.white;
        }
    }
}
