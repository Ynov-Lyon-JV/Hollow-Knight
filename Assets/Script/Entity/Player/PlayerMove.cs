using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    [NonSerialized]
    public ControllerMove controllerMove;
    private ControllerHealth controllerHealth;
    private ControllerAnimation controllerAnimation;
    private ControllerAnimation controllerAnimationAttack;

    private Vector2 moveIput;

    private Rigidbody2D rigidbody2D;
    private bool isJump;
    private bool canIsJump;

    private int timeJump = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Une instance de l'interface de la vie est déjà existante!");
            return;
        }
        instance = this;

        moveIput.x = 0;
        controllerMove = transform.GetComponent<ControllerMove>();
        controllerHealth = transform.GetComponent<ControllerHealth>();
        controllerAnimation = transform.GetComponent<ControllerAnimation>();
        controllerAnimationAttack = transform.Find("Attack").GetComponent<ControllerAnimation>();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    public void TUpdate()
    {

        moveIput.x = Math.Sign(Input.GetAxisRaw("Horizontal"));


        if (Input.GetButtonDown(Dico.Get("BUTTON_JUMP")) && controllerMove.isGroundedLastFrame)
        {
            canIsJump = true;
            isJump = true;
            timeJump = 3;
            controllerMove.jumpTimeCompteur = 0;
        }
        else
        {

            if (Input.GetButton(Dico.Get("BUTTON_JUMP")) && canIsJump)
            {
                isJump = true;
                timeJump--;
            }
            else
            {
                isJump = false;
                canIsJump = false;
            }
        }

        if (Input.GetButtonDown(Dico.Get("BUTTON_ATTACK")))
        {
            if (!controllerMove.isDash && controllerHealth.Attack())
            {
                controllerAnimationAttack.AnimationPlay(Dico.Get("ANIM_PLAYER_ATTACK"));

            }
        }

        if (Input.GetButtonDown(Dico.Get("BUTTON_DASH")))
        {
            controllerMove.Dash();
        }

        controllerAnimation.ChangeAnimationState(Dico.Get(GetAnimeState()));
    }

    private string GetAnimeState()
    {
        string animeState = "";


        if (controllerMove.isDash)
        {
            animeState = "ANIM_PLAYER_DASH";
        }
        else if (controllerMove.IsGrounded())
        {
            if (moveIput.sqrMagnitude == 1)
            {
                animeState = "ANIM_PLAYER_RUN";
            }
            else
            {
                animeState = "ANIM_PLAYER_IDLE";
            }
        }
        else if (rigidbody2D.velocity.y < 0.01f)
        {
            animeState = "ANIM_PLAYER_LAND";
            canIsJump = false;
        }
        else if (rigidbody2D.velocity.y > 0.01f)
        {
            animeState = "ANIM_PLAYER_JUMP";
        }
        return animeState;
    }

    public void Move()
    {
        if (timeJump < 0 && controllerMove.IsGrounded())
        {
            isJump = false;
            canIsJump = false;
        }
        controllerMove.Move(moveIput.x, isJump);

    }

}
