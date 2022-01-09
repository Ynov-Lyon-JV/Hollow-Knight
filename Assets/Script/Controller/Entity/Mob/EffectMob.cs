
using System;
using System.Collections;
using UnityEngine;

public abstract class EffectMob : Entity
{

    public override IEnumerator EffectTakeDamageColor()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.2f);
            controllerHealth.renderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            controllerHealth.renderer.color = Color.white;
        }
    }


    public override void EffectTakeDamage()
    {

        StartCoroutine(EffectTakeDamageColor());
        if (controllerHealth.Health > 0)
        {
            SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_ENEMY_DOMAGE"), 0.4F);
            Detect = true;
        }
        else
        {
            SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_ENEMY_DEATH"), 0.4F);
        }
    }


}



