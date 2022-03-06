using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayer : Entity
{

    public override void EffectJump()
    {
        SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_PLAYER_JUMP"), 0.3F);
        ParticuleController.PlayParticleEffect("DustJumpParticles", transform.Find("Feet"));
    }

    public override void EffectGrounded()
    {
        SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_PLAYER_LANDING"), 0.2F);
        ParticuleController.PlayParticleEffect("DustLandParticles", transform.Find("Feet"));
    }

    public override void EffectTakeDamage()
    {

        StartCoroutine(EffectInvulnerability());
        SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_PLAYER_DOMAGE"), 0.33F);
    }
    public override IEnumerator EffectInvulnerability()
    {


        controllerHealth.timeInvulnerable = controllerHealth.timeInvulnerableStart;
        while(controllerHealth.timeInvulnerable > 0)
        {
            yield return new WaitForSeconds(0.15f);
            controllerHealth.renderer.color = Color.black;
            yield return new WaitForSeconds(0.15f);
            controllerHealth.renderer.color = Color.white;
        }

        controllerHealth.renderer.color = Color.white;
    }


}
