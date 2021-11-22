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

    [SerializeField]
    [Tooltip("De base: 0.3")]
    private float checkRadius;

    [SerializeField]
    [Tooltip("De base: Platform")]
    private LayerMask whatisGround;

    [SerializeField]
    [Tooltip("Le temps de jump. De base: 15")]
    private float jumpTime;

    private float jumpTimeCompteur = 0;


    [SerializeField]
    private Collider2D feetPos;

    public Vector2 moveIput;
    private new Rigidbody2D rigidbody2D;


    [SerializeField]
    public float direction;
    [NonSerialized]
    public bool directionLock;

    private MobMove mobMove;

    public bool isGrounded;
    public bool isKnockback = false;
    public bool canMove = true;
    public bool isDash = false;

    [SerializeField]
    private float startTimeBtwDash;
    private float timeBtwDash = 0;

    public bool IsGrounded()
    {
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
        }
    }

    void FixedUpdate()
    {
        if (mobMove)
        {
            mobMove.Move();
        }
    }
    public void Move(float velocityX, bool isJump = false)
    {
        if (canMove)
        {
            moveIput.x = velocityX;
        }
        else
        {
            moveIput.x = 0;
        }
        moveIput.y++;
        rigidbody2D.velocity = new Vector2(moveIput.x * speed, rigidbody2D.velocity.y);
        Flip();

        if (isJump)
        {
            if (IsGrounded())
            {
                jumpTimeCompteur = jumpTime;
                if (mobMove)
                {
                    //Play son mob mobMove.SonJump();
                }
                else
                {
                    SounfEffectsController.playSound(Dico.Get("SOUND_PLAYER_JUMP"));
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
        if (!mobMove && isKnockback)
        {
            isDash = false;
            rigidbody2D.velocity = new Vector2(-direction * 30f, 15f);
        }
        if (!mobMove && isDash)
        {
            rigidbody2D.velocity = new Vector2(direction * 30f, 5f);
        }

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
        direction = -this.transform.localScale.x;

    }
    public void Dash()
    {
        if (timeBtwDash <= 0)
        {
            StartCoroutine(CaroutineDash());
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
}
