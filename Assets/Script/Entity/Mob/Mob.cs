using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Entity
{

    #region Move

    private int timeDirectionLock = 0;


    public override void Move()
    {
        MoveBasic();
    }

    protected void MoveBasic()
    {
        if (!controllerMove.IsGrounded() && timeDirectionLock <= 0)
        {
            Flip();
            timeDirectionLock = 3; //Permet d'éviter les bugs
        }
        else if (timeDirectionLock > 0)
        {
            timeDirectionLock--;
        }
        controllerMove.Move(controllerMove.direction / controllerMove.slowTempo);
    }

    public void Flip()
    {
        controllerMove.direction = -controllerMove.direction;
        controllerMove.slowTempo = 100;
    }
    #endregion

    #region Detect
    [SerializeField]
    [Tooltip("De base: 2")]
    private float startTimeDetect = 2;
    private float timeDetect = 0;

    private bool detect;
    public virtual bool Detect
    {
        get { return detect; }
        set
        {
            detect = value;
            transform.Find("Detect").gameObject.SetActive(value);

            if (value)
            {
                timeDetect = startTimeDetect;
            }
        }
    }

    void Update()
    {
        if (timeDetect > 0)
        {
            timeDetect -= Time.deltaTime;
        }
        else if (Detect)
        {
            Detect = false;
        }
    }
    #endregion


    #region Destroy

    public override void Destroy()
    {
        //transform.Find("PVParticle").GetComponent<ParticleSystem>().Play();
        Destroy(GetComponent<ControllerMove>());
        Destroy(GetComponent<ControllerHealth>());
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GetComponent<FadeOut>().canFade = true;
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
    #endregion



}
