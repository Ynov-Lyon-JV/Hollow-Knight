using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSpell : MonoBehaviour
{
    public static ControllerSpell instance;
    public List<Spell> spells = new List<Spell>();

    public GameObject UI_Spells = null;


    private int posX = 250;
    private int posY = -200;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Une instance des spells est déjà existante!");
            return;
        }
        instance = this;

        foreach (Spell spell in GetComponentsInChildren<Spell>())
        {
            spells.Add(spell);
        }

    }

    internal void StopSpell(string nameSpell)
    {
        Spell spell = spells.Find(spell => spell.name == nameSpell);
        if (spell == null)
            return;
        spell.isSelect = false;
    }

    public void GetSpell(string nameSpell)
    {
        CleanSelect();
        Select(nameSpell);
    }

    public bool IsProctect(int damage)
    {
        foreach (Spell spell in spells)
        {
            if (spell.isSelect && spell.type == Spell.Type.Defense)
            {
                return spell.UseSpell(damage);
            }
        }
        return false;
    }

    private void CleanSelect()
    {
        foreach (Spell spell in spells)
        {
            spell.isSelect = false;
        }

        foreach (GameObject gObject in GameObject.FindGameObjectsWithTag("On"))
        {
            if (gObject.name == "On")
            {
                gObject.SetActive(false);
            }
        }
    }

    internal void XpSpell(string nameSpell)
    {
        Spell spell = spells.Find(spell => spell.name == nameSpell);
        if (spell == null)
            return;
        spell.xp++;
        if (spell.xp == 1)
        {
            spell.isUnlock = true;
            AfficheUI();
        }
    }

    private void Select(string nameSpell)
    {
        Spell spell = spells.Find(spell => spell.name == nameSpell);
        if (spell == null)
            return;
        spell.isSelect = true;
    }


    public void AfficheUI()
    {
        int x = 1, y = 1;

        foreach (Spell spell in spells)
        {
            if (!spell.isUnlock)
                continue;
            GameObject spellNew = Instantiate(Resources.Load<GameObject>("Player/Spells/Spell"), transform.position, Quaternion.identity, UI_Spells.transform);
            spellNew.name = spell.name;
            spellNew.transform.Find("Icon").GetComponent<Image>().sprite = spell.spriteIcon;

            spellNew.transform.position = new Vector3(posX * x + 200, posY * y + 900);

            if (x >= 5)
            {
                y += 2;
                x = 1;
            }
            else
            {
                x += 2;
            }
        }

    }
}
