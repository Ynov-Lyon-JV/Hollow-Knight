using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerMove : MonoBehaviour
{
    private ControllerMove instance;

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


    [NonSerialized]
    public float jumpTimeCompteur = 0;


    [SerializeField]
    private Collider2D feetPos;
    private Transform feetPosEyes = null;

    public Vector2 moveIput;
    private new Rigidbody2D rigidbody2D;


    [SerializeField]
    public float direction;
    private float directionDamage;


    [NonSerialized]
    public float slowTempo = 1;

    [NonSerialized]
    public bool directionLock;

    private MobMove mobMove;

    public bool isGrounded = false;
    public bool isKnockback = false;
    public bool canMove = true;
    public bool isDash = false;

    [SerializeField]
    private float startTimeBtwDash;
    private float timeBtwDash = 0;

    public bool isGroundedLastFrame = false;
    public bool load = false;

    public bool IsGrounded()
    {
        isGroundedLastFrame = isGrounded;
        if (feetPosEyes != null)
        {
            isGrounded = Physics2D.OverlapCircle(feetPosEyes.position, checkRadius, whatisGround);
            if (!isGrounded)
                return isGrounded;
        }
        isGrounded = Physics2D.OverlapCircle(feetPos.transform.position, checkRadius, whatisGround);

        return isGrounded;
    }

    private void Awake()
    {
        speed = speedBase;
        moveIput = new Vector2();
        feetPos = transform.GetComponent<Collider2D>();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        mobMove = transform.GetComponent<MobMove>();
        if (mobMove)
        {
            mobMove.CM = this;
            feetPosEyes = transform.Find("EyesGround");
        }
    }


    #region Move
    void FixedUpdate()
    {
        if (mobMove)
        {
            mobMove.Move();
        }
    }
    public void Move(float velocityX, bool isJump = false)
    {
        slowTempo = 1;
        if (canMove)
        {
            moveIput.x = velocityX;
        }
        else
        {
            moveIput.x = 0;
        }
        rigidbody2D.velocity = new Vector2(moveIput.x * speed, rigidbody2D.velocity.y);
        Flip();


        if (isKnockback)
        {
            isDash = false;
            rigidbody2D.velocity = new Vector2(directionDamage * 30f, 15f);
            jumpTimeCompteur = 0f;
        }
        else if (isDash)
        {
            rigidbody2D.velocity = new Vector2(direction * 30f, 5f);
            jumpTimeCompteur = 0f;
        }
        else if (isJump)
        {
            if (IsGrounded() && jumpTimeCompteur <= 0f)
            {
                jumpTimeCompteur = jumpTime;
                if (mobMove)
                {
                    //Play son mob mobMove.SonJump();
                }
                else
                {
                    SounfEffectsController.playSoundEffect(Dico.Get("SOUND_PLAYER_JUMP"), 0.5F);
                    ParticuleController.playParticleEffect("DustJumpParticles", this.transform);
                }
            }
            if (jumpTimeCompteur > 0f)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                jumpTimeCompteur -= 1f;
            }
            else
            {
                jumpTimeCompteur = 0f;
            }
        }


        if (!mobMove && IsGrounded() && !isGroundedLastFrame)
        {
            ParticuleController.playParticleEffect("DustLandParticles", this.transform);
        }
        isGroundedLastFrame = isGrounded;

        timeBtwDash -= Time.deltaTime;
    }

    private void Flip()
    {
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
        }
    }

    IEnumerator CaroutineDash()
    {
        timeBtwDash = startTimeBtwDash;
        canMove = false;
        isDash = true;
        yield return new WaitForSeconds(0.3f);
        isDash = false;
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }
    #endregion

    #region Knockback
    public void Knockback(float directionDamage)
    {
        this.directionDamage = directionDamage;
        StartCoroutine(CaroutineKnockback());
    }
    IEnumerator CaroutineKnockback()
    {
        canMove = false;
        isKnockback = true;
        yield return new WaitForSeconds(0.15f);
        isKnockback = false;
        yield return new WaitForSeconds(0.15f);
        canMove = true;
    }
    #endregion
}
