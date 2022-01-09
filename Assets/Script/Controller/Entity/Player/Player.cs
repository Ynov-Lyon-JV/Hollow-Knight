using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EffectPlayer
{
    PlayerMove playerMove;



    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }


    public override void Move()
    {
        playerMove.Move();
    }

    public override void FrameUpdate()
    {
        playerMove.FrameUpdate();
    }

    public override void Destroy()
    {
        controllerHealth.Health = 3;
        controllerMove.isKnockback = false;
        GetComponent<ControllerSpawn>().RespawnNear();
    }


    public override void UpdatePlayerHealth()
    {
        InterfaceHealth.instance.Change(controllerHealth.Health);
    }


}
