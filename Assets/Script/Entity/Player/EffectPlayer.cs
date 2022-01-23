using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayer : Entity
{

    public override void EffectJump()
    {
        SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_PLAYER_JUMP"), 0.3F);
        ParticuleController.PlayParticleEffect("DustJumpParticles", this.transform);
    }

    public override void EffectGrounded()
    {
        SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_PLAYER_LANDING"), 0.2F);
        ParticuleController.PlayParticleEffect("DustLandParticles", this.transform);
    }

    public override void EffectTakeDamage()
    {

        StartCoroutine(EffectInvulnerability());
        SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_PLAYER_DOMAGE"), 0.33F);
    }
    public override IEnumerator EffectInvulnerability()
    {


        controllerHealth.timeInvulnerable = controllerHealth.timeInvulnerableStart;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            controllerHealth.renderer.color = Color.red / 2;
            yield return new WaitForSeconds(0.2f);
            controllerHealth.renderer.color = Color.white;
        }
    }


}
