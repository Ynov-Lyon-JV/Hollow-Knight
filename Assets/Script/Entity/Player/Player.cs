using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EffectPlayer
{
    PlayerMove playerMove;
    ControllerSpell controllerSpell;

    public static Player instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Une instance de l'interface de la vie est déjà existante!");
            return;
        }
        instance = this;
        playerMove = GetComponent<PlayerMove>();
        controllerSpell = GetComponentInChildren<ControllerSpell>();
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

    public override bool IsProtect(int damage)
    {
        if (controllerSpell == true)
        {
            if (controllerSpell.IsProctect(damage))
                return true;
        }
        return false;
    }


    public void Pause()
    {
        controllerMove.enabled = false;
        controllerHealth.enabled = false;
    }
    public void Resume()
    {
        controllerMove.enabled = true;
        controllerHealth.enabled = true;
    }

}
