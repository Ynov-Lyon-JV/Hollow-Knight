using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controller_Move : MonoBehaviour
{
    [Tooltip("La vitesse du gameObject. De base: 9")]
    public float speed;
    [Tooltip("La puissance du jump. De base: 20")]
    public float jumpForce;

    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatisGround;

    private Vector2 moveIput;
    private new Rigidbody2D rigidbody2D;
    private new Collider2D collider2D;

    [Tooltip("Le temps de jump. De base: 14")]
    public float jumpTime;
    private float jumpTimeCompteur;

    private void Awake()
    {
        jumpTimeCompteur = 0;
        moveIput = new Vector2();
        feetPos = transform.GetComponentInChildren<Transform>();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        collider2D = transform.GetComponent<CapsuleCollider2D>();
    }

    public void Move(float velocityX, bool isJump)
    {
        moveIput.x = velocityX;
        rigidbody2D.velocity = new Vector2(moveIput.x * speed, rigidbody2D.velocity.y);
        Flip();

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatisGround);

        if (isJump)
        {
            if (isGrounded)
            {
                jumpTimeCompteur = jumpTime;
            }
            if (jumpTimeCompteur > 0f)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                jumpTimeCompteur -= 1f;
                SounfEffectsController.playSound("Audio/SoundEffects/SoundEffect_Jump");
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
    
    }

}
