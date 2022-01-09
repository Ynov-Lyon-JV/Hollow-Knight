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
    public virtual bool Detect { get; set; }

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

    public virtual IEnumerator EffectTakeDamageColor()
    {
        return null;
    }
    #endregion


}
