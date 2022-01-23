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

    public Type type;

    private Player player;

    [SerializeField]
    private float startTimeBtwSpell;
    protected float timeBtwSpell = 0;
    protected bool isActivate = false;

    private void Awake()
    {
        player = GetComponent<Player>();
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
        timeBtwSpell = startTimeBtwSpell;
        isActivate = true;
    }
    public virtual bool UseSpell(int damage = 0)
    {
        return false;
    }

    public virtual bool Verif()
    {
        if (!isActivate && Input.GetButtonDown(Dico.Get("BUTTON_SPELL")) && timeBtwSpell <= 0)
        {
            return true;
        }
        return false;
    }
}
