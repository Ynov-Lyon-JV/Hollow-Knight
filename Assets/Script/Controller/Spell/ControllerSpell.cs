using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControllerSpell : MonoBehaviour
{
    List<Spell> spells = new List<Spell>();

    private void Awake()
    {
        GetSpell("SPELL_SLIME");
    }

    public void GetSpell(string spell)
    {
        switch (spell)
        {
            case "SPELL_BAT":
                break;
            case "SPELL_SLIME":
                Instantiate(Resources.Load("Player/Spells/Spell_Slime"), transform);
                spells.Add(GetComponentInChildren<SpellSlime>());
                break;
        }
    }
    
    public bool IsProctect(int damage)
    {
        foreach(Spell spell in spells)
        {
            if(spell.type == Spell.Type.Defense)
            {
                return spell.UseSpell(damage);
            }
        }
        return false;
    }
}
