using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private ControllerMove controllerMove;
    private ControllerHealth controllerHealth;
    private ControllerAnimation controllerAnimation;
    private ControllerAnimation controllerAnimationAttack;

    private Vector2 moveIput;

    private new Rigidbody2D rigidbody2D;
    private bool isJump;
    private bool canIsJump;


    private void Awake()
    {
        moveIput.x = 0;
        controllerMove = transform.GetComponent<ControllerMove>();
        controllerHealth = transform.GetComponent<ControllerHealth>();
        controllerAnimation = transform.GetComponent<ControllerAnimation>();
        controllerAnimationAttack = transform.Find("Attack").GetComponent<ControllerAnimation>();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetButtonDown(Dico.Get("BUTTON_ATTACK")))
        {
            if (controllerHealth.Attack())
            {
                controllerAnimationAttack.AnimationPlay(Dico.Get("ANIM_PLAYER_ATTACK"));
            }
        }
    }

    void FixedUpdate()
    {
        controllerMove.Move(moveIput.x, isJump);
    }



}
