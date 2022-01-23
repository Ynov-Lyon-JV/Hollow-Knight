using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHealth : MonoBehaviour
{

    [SerializeField]
    protected int health;

    [SerializeField]
    protected int maxHealth = 3;
    public virtual int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            entity.UpdatePlayerHealth();
        }
    }

    public virtual int MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
        }
    }

    [SerializeField]
    [Tooltip("De base: 0.6")]
    private float startDazedTime;
    private float dazedTime;


    public new SpriteRenderer renderer;

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


    public float timeInvulnerableStart = 0.4f;
    public float timeInvulnerable = 0;
    private Entity entity;

    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();



        entity = GetComponent<Entity>();
        entity.controllerHealth = this;


        MaxHealth = MaxHealth;
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (dazedTime > 0)
        {
            entity.controllerMove.speed = 0;
            dazedTime -= Time.deltaTime;
            if (dazedTime <= 0)
            {
                entity.controllerMove.speed = entity.controllerMove.speedBase;
            }
        }

        if (Health <= 0)
        {
            entity.Destroy();
        }

        timeInvulnerable -= Time.deltaTime;
        timeBtwAttack -= Time.deltaTime;
    }

    public void TakeDamage(int damage, bool particule = true, float directionDamage = 0, bool canProtect = true)
    {


        if (timeInvulnerable <= 0 || !canProtect)
        {
            dazedTime = startDazedTime;
            if (particule)
                Instantiate(bloodEffect, transform.position, Quaternion.identity);


            if (!canProtect)
                Health -= damage;
            else if (!entity.IsProtect(damage))
                Health -= damage;

            if (directionDamage != 0)
            {
                entity.controllerMove.Knockback(directionDamage);
            }

            entity.EffectTakeDamage();


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


    public void TriggerMob(Collider2D collision, string tag)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == tag)
        {

            if (timeBtwAttack <= 0)
            {
                float knockback = 0;
                if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
                {
                    knockback = Dico.CalculeDirection(collision.gameObject.transform, transform);
                }
                collision.gameObject.GetComponent<ControllerHealth>().TakeDamage(damage, true, knockback);
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
