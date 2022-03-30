using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlime : Spell
{
    private new SpriteRenderer renderer;
    private void Awake()
    {
        type = Type.Defense;
        renderer = GetComponent<SpriteRenderer>();
        name = "Spell_Slime";

        Clean();
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
            Clean();
            return true;
        }
        return false;
    }

    public override void Clean()
    {
        isActivate = false;
        renderer.enabled = false;
    }
}
