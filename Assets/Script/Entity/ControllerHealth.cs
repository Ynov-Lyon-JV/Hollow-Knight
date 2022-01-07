using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHealth : MonoBehaviour
{

    [SerializeField]
    private int health;

    [SerializeField]
    private int maxHealth;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (isPlayer)
                InterfaceHealth.instance.Change(health);
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            if (isPlayer)
                InterfaceHealth.instance.maxHealth = maxHealth;
        }
    }

    [SerializeField]
    [Tooltip("De base: 0.6")]
    private float startDazedTime;
    private float dazedTime;

    private ControllerMove controllerMove;
    private ControllerSpawn controllerSpawn;

    private SpriteRenderer renderer;

    [SerializeField]
    private GameObject bloodEffect;

    public bool isPlayer;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRangeX;
    public float attackRangeY;
    public int damage;

    public float timeBtwAttack = 0;
    public float startTimeBtwAttack;


    private bool invulnerable = false;

    // Start is called before the first frame update
    void Awake()
    {
        controllerMove = GetComponent<ControllerMove>();
        controllerSpawn = GetComponent<ControllerSpawn>();
        renderer = GetComponent<SpriteRenderer>();

        MaxHealth = MaxHealth;
        Health = MaxHealth;
        if (isPlayer)
            InterfaceHealth.instance.Change(Health);
    }

    // Update is called once per frame
    void Update()
    {

        if (dazedTime > 0)
        {
            controllerMove.speed = 0;
            dazedTime -= Time.deltaTime;
            if (dazedTime <= 0)
            {
                controllerMove.speed = controllerMove.speedBase;
            }
        }

        if (Health <= 0)
        {
            if (isPlayer)
            {
                Health = 3;
                controllerMove.isKnockback = false;
                controllerSpawn.RespawnNear();
            }
            else
            {
                GetComponent<MobMove>().Destroy();
            }
        }

        timeBtwAttack -= Time.deltaTime;
    }

    public void TakeDamage(int damage, bool particule = true, float directionDamage = 0)
    {

        
        if (!invulnerable)
        {
            if (LayerMask.LayerToName(gameObject.layer) == "Secret")
            {
                //Health -= damage;
                GetComponent<FadeOut>().canFade = true;
            }
            else { 
            dazedTime = startDazedTime;
            if (particule)
                Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Health -= damage;
            StartCoroutine(TakeDamageColor());
            if (directionDamage != 0)
            {
                controllerMove.Knockback(directionDamage);
            }
                if (isPlayer)
                {
                    SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_PLAYER_DOMAGE"), 0.33F);
                }
                else
                {
                    if (health > 0) {
                        SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_ENEMY_DOMAGE"), 0.4F);
                        controllerMove.mobMove.Detect = true; }
                    else { 
                        SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_ENEMY_DEATH"), 0.4F);}
                }
        }
        }
    }


    IEnumerator TakeDamageColor()
    {
        if (isPlayer)
        {
            invulnerable = true;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.2f);
                renderer.color = Color.red / 2;
                yield return new WaitForSeconds(0.2f);
                renderer.color = Color.white;
            }
            invulnerable = false;
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(0.2f);
                renderer.color = Color.red;
                yield return new WaitForSeconds(0.2f);
                renderer.color = Color.white;
            }
        }
    }


    public bool Attack()
    {
        if (timeBtwAttack <= 0)
        {
            Collider2D[] ennemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
            for (int i = 0; i < ennemiesToDamage.Length; i++)
            {
                ennemiesToDamage[i].GetComponent<ControllerHealth>().TakeDamage(damage);
            }
            timeBtwAttack = startTimeBtwAttack;
            
                SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_PLAYER_SWORD"), 0.1F);
            return true;
        }
        return false;
    }


    public void TriggerMob(Collider2D collision)
    {
        if (!isPlayer && collision.gameObject.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                collision.gameObject.GetComponent<ControllerHealth>().TakeDamage(damage, true, Dico.CalculeDirection(collision.gameObject.transform,transform));
                timeBtwAttack = startTimeBtwAttack;
            }

        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }

}
