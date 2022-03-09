using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Entity
{

    #region Move

    protected bool isJump = false;

    private int timeDirectionLock = 0;

    private void Awake()
    {
        gDedect = transform.Find("Detect").gameObject;
    }
    public override void Move()
    {
        MoveBasic();
        controllerMove.Move(controllerMove.direction / controllerMove.slowTempo, isJump);
    }

    protected void MoveBasic()
    {
        if (!controllerMove.IsGrounded() && timeDirectionLock <= 0)
        {
            Flip();
            timeDirectionLock = 5; //Permet d'éviter les bugs
        }
        else if (timeDirectionLock > 0)
        {
            timeDirectionLock--;
        }
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
    public float timeDetect = 0;

    private GameObject gDedect;
    private bool detect;
    public virtual bool Detect
    {
        get { return detect; }
        set
        {
            detect = value;
            if(gDedect)
            gDedect.SetActive(value);

            if (value)
            {
                timeDetect = startTimeDetect;
            }
        }
    }
    public Transform whoDetect;

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
        GameObject xp = Instantiate(Resources.Load<GameObject>("Level/Follet/Follet_XP"), transform.position, Quaternion.identity);
        xp.name = "Spell_" + transform.name;
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

        StartCoroutine(EffectInvulnerability());
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


    public override IEnumerator EffectInvulnerability()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.12f);
            controllerHealth.renderer.color = Color.gray;
            yield return new WaitForSeconds(0.12f);
            controllerHealth.renderer.color = Color.white;
        }
    }
    #endregion



}
