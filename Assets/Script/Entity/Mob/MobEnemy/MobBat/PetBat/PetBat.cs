using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBat : Mob
{
    private GameObject player;
    private Chasse chasse;
    private Vector2 vector;
    private bool savePos = true;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chasse = GetComponentInChildren<Chasse>();
    }
    public override void Move()
    {
        if (timeDetect <= 0)
        {
            /*
            if (savePos)
            {
                savePos = false;
                 vector = transform.position;
            }
            Wait();
            savePos = true;*/
            Chasse(Player.instance.transform.Find("Head").transform);
        }
        else
        {
            savePos = true;
            Chasse(whoDetect);
        }
        //base.Move();

        controllerMove.Flip();

    }
    private void Chasse(Transform target)
    {
        chasse.target = target;
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 10 * Time.deltaTime);
        controllerMove.moveIput = chasse.UpdateMove(controllerMove.speed);


    }
    private void Wait()
    {
        float move = Mathf.PingPong(Time.time * 0.2f, 0.1f);
        //controllerMove.Move(move, isJump);

        transform.position = new Vector2(vector.x + move, vector.y + move);
    }
}
