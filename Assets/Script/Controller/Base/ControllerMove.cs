using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerMove : MonoBehaviour
{
    private readonly ControllerMove instance;

    [SerializeField]
    [Tooltip("La vitesse du gameObject. De base: 15 pour le player")]
    public float speedBase;

    public float speed;

    [SerializeField]
    [Tooltip("La puissance du jump. De base: 30")]
    private float jumpForce;

    [Tooltip("De base: 0.3")]
    public float checkRadius;

    [Tooltip("De base: Platform")]
    public LayerMask whatisGround;

    [SerializeField]
    [Tooltip("Le temps de jump. De base: 15")]
    private float jumpTime;


    public float jumpTimeCompteur = 0;


    [SerializeField]
    public Collider2D feetPos;
    private Transform feetPosEyes = null;

    public Vector2 moveIput;
    public new Rigidbody2D rigidbody2D;


    [SerializeField]
    public float direction;
    private float directionDamage;


    [NonSerialized]
    public float slowTempo = 1;

    [NonSerialized]
    public bool directionLock;

    public Entity entity;

    public bool isGrounded = false;
    public bool isKnockback = false;
    public bool canMove = true;
    public bool isDash = false;

    [SerializeField]
    private float startTimeBtwDash;
    private float timeBtwDash = 0;

    public float timeGrounded = 0;
    public bool isGroundedLastFrame = false;
    public bool load = false;


    private void Awake()
    {
        speed = speedBase;
        moveIput = new Vector2();
        feetPos = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        entity = GetComponent<Entity>();
        entity.controllerMove = this;
    }


    public bool IsGrounded()
    {

        isGrounded = Physics2D.OverlapCircle(new Vector2(feetPos.bounds.center.x,feetPos.bounds.min.y), checkRadius, whatisGround);
        if (isGrounded)
        {
            timeGrounded = 0.12f;
        }
        return isGrounded;
    }

    #region Move
    private void Update()
    {
        entity.FrameUpdate();
    }

    void FixedUpdate()
    {
        entity.Move();
    }

    public void Move(float velocityX, bool isJump = false)
    {
        slowTempo = 1;
        if (canMove)
        {
            moveIput.x = velocityX;
            rigidbody2D.velocity = new Vector2(moveIput.x * speed, rigidbody2D.velocity.y);
            Flip();
        }


        if (isKnockback)
        {
            isDash = false;
            rigidbody2D.velocity = vectorKnockback;
            //jumpTimeCompteur = 0f;
        }
        else if (isDash)
        {
            rigidbody2D.velocity = new Vector2(direction * 30f,2f);
            jumpTimeCompteur = 0f;
        }
        else if (isJump)
        {
            if (timeGrounded > 0 && jumpTimeCompteur <= 0f)
            {
                jumpTimeCompteur = jumpTime;
                if(isGroundedLastFrame)
                    entity.EffectJump();
            }
            if (jumpTimeCompteur > 0f)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                jumpTimeCompteur -= 1f;
                timeGrounded = 0;
            }
        }


        if (!isGroundedLastFrame && IsGrounded())
        {
            entity.EffectGrounded();
        }
        isGroundedLastFrame = IsGrounded();

        timeGrounded -= Time.deltaTime;
        timeBtwDash -= Time.deltaTime;
    }


    public void Flip()
    {
        //Pour ne pas se retourner lors du attaque
        if (GetComponent<ControllerHealth>().timeBtwAttack > GetComponent<ControllerHealth>().startTimeBtwAttack - 0.3f)
            return;

        if (moveIput.x < 0f)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (moveIput.x > 0f)
        {
            this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        direction = -Math.Sign(this.transform.localScale.x);
    }
    #endregion

    #region Dash
    public void Dash()
    {
        if (timeBtwDash <= 0)
        {
            StartCoroutine(CaroutineDash());
            transform.Find("Dash").GetComponent<ControllerAnimation>().AnimationPlay(Dico.Get("ANIM_PLAYER_DASH_EFFECT"));

            SounfEffectsController.PlaySoundEffect(Dico.Get("SOUND_PLAYER_DASH"), 0.3F);
        }
    }

    IEnumerator CaroutineDash()
    {
        timeBtwDash = startTimeBtwDash;
        canMove = false;
        isDash = true;
        yield return new WaitForSeconds(0.2f);
        isDash = false;
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }
    #endregion

    #region Knockback
    public void Knockback(Vector2 directionDamage)
    {
        vectorKnockback = directionDamage;
        StartCoroutine(CaroutineKnockback());
    }
    IEnumerator CaroutineKnockback()
    {
        //canMove = false;
        isKnockback = true;
        yield return new WaitForSeconds(0.15f);
        isKnockback = false;
        yield return new WaitForSeconds(0.15f);
        canMove = true;
    }
    #endregion

    public ProjectileBehaviour ProjectilePrefab;
    public Transform FireOffset;
    public Vector2 vectorKnockback = new Vector2(30f, 15f);

    public void Fire()
    {
        ProjectileBehaviour pb = Instantiate(ProjectilePrefab, FireOffset.position, new Quaternion());
        if (direction > 0)
        {
            pb.speed = -20f;
        }
        else
        {
            pb.speed = 20f;
        }

    }

    public BombBehaviour BombPrefab;
    public void Bomb()
	{
        Vector3 BombPosition = FireOffset.position;
        BombPosition.x = BombPosition.x - 1;
        BombBehaviour bb = Instantiate(BombPrefab, FireOffset.position, new Quaternion());

    }
}
