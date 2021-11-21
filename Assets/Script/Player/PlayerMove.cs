using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private ControllerMove controllerMove;
    private ControllerAnimation controllerAnimation;
    private ControllerAnimation controllerAnimationAttack;

    private Vector2 moveIput;

    private new Rigidbody2D rigidbody2D;
    private bool isJump;
    private bool canIsJump;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRangeX;
    public float attackRangeY;
    public int damage;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    private void Awake()
    {
        InitAnimation();
        moveIput.x = 0;
        controllerMove = transform.GetComponent<ControllerMove>();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }
    private void InitAnimation()
    {
        controllerAnimation = transform.GetComponent<ControllerAnimation>();
        controllerAnimationAttack = transform.Find("Attack").GetComponent<ControllerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rigidbody2D.velocity);    
        moveIput.x = Math.Sign(Input.GetAxisRaw("Horizontal"));

        if (controllerMove.IsGrounded())
        {
            if (moveIput.sqrMagnitude == 1)
            {
                controllerAnimation.ChangeAnimationState(Dico.Get("ANIM_PLAYER_RUN"));
            }
            else
            {
                controllerAnimation.ChangeAnimationState(Dico.Get("ANIM_PLAYER_IDLE"));
            }
        }
        else if (rigidbody2D.velocity.y < 0.01f)
        {
            controllerAnimation.ChangeAnimationState(Dico.Get("ANIM_PLAYER_LAND"));
            canIsJump = false;
        }
        else if (rigidbody2D.velocity.y > 0.01f)
        {
            controllerAnimation.ChangeAnimationState(Dico.Get("ANIM_PLAYER_JUMP"));
        }

        if (Input.GetButtonDown(Dico.Get("BUTTON_JUMP")) && controllerMove.IsGrounded())
        {
            canIsJump = true;
        }
        else
        {

            if (Input.GetButton(Dico.Get("BUTTON_JUMP")) && canIsJump)
            {
                isJump = true;
            }
            else
            {
                isJump = false;
                canIsJump = false;
            }
        }

        if (timeBtwAttack <= 0)
        {
            if (Input.GetButtonDown(Dico.Get("BUTTON_ATTACK")))
            {
                controllerAnimationAttack.AnimationPlay(Dico.Get("ANIM_PLAYER_ATTACK"));
                Collider2D[] ennemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
                for (int i = 0; i < ennemiesToDamage.Length; i++)
                {
                    ennemiesToDamage[i].GetComponent<ControllerHealth>().TakeDamage(damage);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        controllerMove.Move(moveIput.x, isJump);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }


}
