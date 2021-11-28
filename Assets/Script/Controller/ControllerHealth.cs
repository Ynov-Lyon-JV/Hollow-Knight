using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHealth : MonoBehaviour
{

    [SerializeField]
    private int health;

    [SerializeField]
    [Tooltip("De base: 0.6")]
    private float startDazedTime;
    private float dazedTime;

    private ControllerMove controllerMove;
    private ControllerSpawn controllerSpawn;

    private new SpriteRenderer renderer;

    [SerializeField]
    private GameObject bloodEffect;

    public bool isPlayer;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRangeX;
    public float attackRangeY;
    public int damage;

    private float timeBtwAttack = 0;
    public float startTimeBtwAttack;


    private new Rigidbody2D rigidbody2D;
    private bool invulnerable = false;

    // Start is called before the first frame update
    void Awake()
    {
        controllerMove = transform.GetComponent<ControllerMove>();
        controllerSpawn = transform.GetComponent<ControllerSpawn>();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        renderer = transform.GetComponent<SpriteRenderer>();
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

        if (health <= 0)
        {
            if (isPlayer)
            {
                health = 3;
                controllerMove.isKnockback = false;
                controllerSpawn.RespawnNear();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        timeBtwAttack -= Time.deltaTime;
    }

    public void TakeDamage(int damage,bool knockback = false, float directionDamage = 0)
    {
        if (!invulnerable)
        {
            dazedTime = startDazedTime;
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            health -= damage;
            StartCoroutine(TakeDamageColor());
            if (knockback)
            {
                controllerMove.Knockback(directionDamage);
            }
            //StartCoroutine(Knockback()); Toujours utile ?

            if (isPlayer)
            {
                InterfaceHealth.instance.health = health;
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
            return true;
        }
        return false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlayer && collision.gameObject.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                collision.gameObject.GetComponent<ControllerHealth>().TakeDamage(damage,true,CalculeDirectionDamage(collision.gameObject.transform));
                timeBtwAttack = startTimeBtwAttack;
            }

        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }

    private float CalculeDirectionDamage(Transform target)
    {
        ControllerBorder controllerBorder;
        return Math.Sign(target.position.x - transform.position.x);
    }
}
