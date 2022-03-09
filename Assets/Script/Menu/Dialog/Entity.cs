using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public virtual void FrameUpdate()
    {

    }

    #region ControllerMove

    [NonSerialized]
    public ControllerMove controllerMove;
    public virtual void Move()
    {

    }

    #endregion

    #region ControllerHealth

    [NonSerialized]
    public ControllerHealth controllerHealth;

    public virtual void UpdatePlayerHealth()
    {
    }

    public virtual void Destroy()
    {
    }


    #endregion

    #region Effect
    public virtual void EffectJump()
    {

    }
    public virtual void EffectGrounded()
    {

    }
    public virtual void EffectTakeDamage()
    {
    }

    public virtual IEnumerator EffectInvulnerability()
    {
        return null;
    }

    public virtual bool IsProtect(int damage)
    {
        return false;
    }
    #endregion


}
