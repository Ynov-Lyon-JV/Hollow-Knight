using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    public Player player;
    private ControllerAnimation controllerAnimation;
    private ControllerAnimation controllerAnimationAttack;

    private Vector2 moveIput;

    private new Rigidbody2D rigidbody2D;
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
        player = GetComponent<Player>();
        controllerAnimation = transform.GetComponent<ControllerAnimation>();
        controllerAnimationAttack = transform.Find("Attack").GetComponent<ControllerAnimation>();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    public void FrameUpdate()
    {

        moveIput.x = Math.Sign(Input.GetAxisRaw("Horizontal"));


        if (Input.GetButtonDown(Dico.Get("BUTTON_JUMP")) && player.controllerMove.isGroundedLastFrame)
        {
            canIsJump = true;
            isJump = true;
            timeJump = 3;
            player.controllerMove.jumpTimeCompteur = 0;
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
            if (!player.controllerMove.isDash && player.controllerHealth.Attack())
            {
                controllerAnimationAttack.AnimationPlay(Dico.Get("ANIM_PLAYER_ATTACK"));

            }
        }

        if (Input.GetButtonDown(Dico.Get("BUTTON_DASH")))
        {
            player.controllerMove.Dash();
        }
        if (Input.GetButtonDown(Dico.Get("BUTTON_FIRE")))
        {
            player.controllerMove.Fire();
        }

        controllerAnimation.ChangeAnimationState(Dico.Get(GetAnimeState()));
    }

    private string GetAnimeState()
    {
        string animeState = "";


        if (player.controllerMove.isDash)
        {
            animeState = "ANIM_PLAYER_DASH";
        }
        else if (player.controllerMove.IsGrounded())
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
        if (timeJump < 0 && player.controllerMove.IsGrounded())
        {
            isJump = false;
            canIsJump = false;
        }
        player.controllerMove.Move(moveIput.x, isJump);

    }

}
