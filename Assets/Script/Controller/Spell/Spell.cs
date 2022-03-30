using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public enum Type
    {
        Attack,
        Defense,
        Summuner,
        Utility,
    }

    [NonSerialized]
    public Type type;
    [NonSerialized]
    public new string name = "Spell";

    private Player player;

    [SerializeField]
    protected float startTimeBtwSpell;
    protected float timeBtwSpell = 0;
    protected bool isActivate = false;

    public Sprite spriteIcon;

    public int xp;

    private bool IsUnlock = false;

    public bool isUnlock
    {
        get
        {
            return IsUnlock;
        }
        set
        {
            IsUnlock = value;
        }
    }

    private bool IsSelect = false;

    public bool isSelect
    {
        get
        {
            return IsSelect;
        }
        set
        {
            IsSelect = value;
            if (!value)
            {
                Clean();
            }
            else
            {
                InitSpell();
            }
        }
    }

    public virtual void Clean()
    {
    }

    private void Awake()
    {
        player = GetComponent<Player>();
        xp = 0;
    }
    private void Update()
    {
        if (Verif())
        {
            InitSpell();
        }
        if(!isActivate)
            timeBtwSpell -= Time.deltaTime;
    }
    public virtual void InitSpell()
    {
        timeBtwSpell = 0;
        isActivate = true;
    }
    public virtual bool UseSpell(int damage = 0)
    {
        return false;
    }

    public virtual bool Verif()
    {
        if (isSelect && !isActivate && timeBtwSpell <= 0)
        {
            return true;
        }
        return false;
    }
}
