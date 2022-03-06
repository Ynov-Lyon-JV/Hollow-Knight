using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public Player player;
    private ControllerAnimation controllerAnimation;
    private ControllerAnimation controllerAnimationAttack;

    private Vector2 moveIput;

    private new Rigidbody2D rigidbody2D;
    public bool isJump;
    public bool canIsJump;

    public float timePressJump = 0;
    public int timeJump = 0;

    public Transform attack;
    public Transform attackUp;
    public Transform attackDown;

    private void Awake()
    {

        moveIput.x = 0;
        player = GetComponent<Player>();
        controllerAnimation = transform.GetComponent<ControllerAnimation>();
        controllerAnimationAttack = transform.Find("AnimatorAttack").GetComponent<ControllerAnimation>();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    public void FrameUpdate()
    {

        moveIput.x = Math.Sign(Input.GetAxisRaw("Horizontal"));

        timePressJump -= Time.deltaTime;
        if (Input.GetButtonDown(Dico.Get("BUTTON_JUMP"))){
            timePressJump = 0.12f;
        }
        if (timePressJump > 0 && player.controllerMove.timeGrounded > 0)
        {
            //Debug.Log("je saute: " + timePressJump + " et ground : "+ player.controllerMove.timeGrounded);
            timePressJump = 0;
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
            int direction = Math.Sign(Input.GetAxisRaw("Vertical"));
            Transform position = attack;
            int isAttackDown = 0;
            if (direction > 0) { 
                position = attackUp;
            isAttackDown = -1;
        }
        if (direction < 0)
            {

                position = attackDown;
                isAttackDown = 1;
            }

            if (!player.controllerMove.isDash && player.controllerHealth.Attack(position,isAttackDown))
            {
                controllerAnimationAttack.transform.SetPositionAndRotation(position.position, position.rotation);
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
        if (Input.GetButtonDown(Dico.Get("BUTTON_BOMB")))
        {
            player.controllerMove.Bomb();
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
