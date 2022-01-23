using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlime : Spell
{
    private new Renderer renderer;
    private void Awake()
    {
        type = Type.Defense;
        renderer = GetComponent<Renderer>();
    }

    public override bool Verif()
    {
        if (!isActivate && timeBtwSpell <= 0)
        {
            return true;
        }
        return false;
    }

    public override void InitSpell()
    {
        base.InitSpell();
        renderer.enabled = true;
    }

    public override bool UseSpell(int damage)
    {
        if (isActivate)
        {
            isActivate = false;
            renderer.enabled = false;
            return true;
        }
        return false;
    }
}
