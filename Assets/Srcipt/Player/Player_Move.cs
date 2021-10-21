using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{

    public Controller_Move controller;

    private Vector2 moveIput;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    private bool isJump;
    private bool canIsJump;


    private void Awake()
    {
        moveIput.x = 0;
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        moveIput.x = Math.Sign(Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("Speed", moveIput.sqrMagnitude);

        if (controller.isGrounded)
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isFall", false);
        }
        else if (rigidbody2D.velocity.y < 0.01f)
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isFall", true);
            canIsJump = false;
        }
        else if (rigidbody2D.velocity.y > 0.01f)
        {
            animator.SetBool("isJump", true);
            animator.SetBool("isFall", false);
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            canIsJump = true;
        }
        else
        {

            if (Input.GetButton("Jump") && canIsJump)
            {
                isJump = true;
            }
            else
            {
                isJump = false;
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(moveIput.x, isJump);
    }
}
