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

    private float jumpTimeCompteur;


    [SerializeField]
    private Collider2D feetPos;

    private Vector2 moveIput;
    private new Rigidbody2D rigidbody2D;


    [SerializeField]
    public float direction;
    [NonSerialized]
    public bool directionLock;

    private MobMove mobMove;

    public bool isGrounded;

    public bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.transform.position, checkRadius, whatisGround);
        return isGrounded;
    }

    private void Awake()
    {
        speed = speedBase;
        jumpTimeCompteur = 0;
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
        moveIput.x = velocityX;
        rigidbody2D.velocity = new Vector2(moveIput.x * speed, rigidbody2D.velocity.y);
        Flip();

        if (isJump)
        {
            if (IsGrounded())
            {
                jumpTimeCompteur = jumpTime;
                SounfEffectsController.playSound("Audio/SoundEffects/SoundEffect_Jump");
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

}
