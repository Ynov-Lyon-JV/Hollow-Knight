using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMushroom : Spell
{
    private new Renderer renderer;
    private GameObject pet;
    private void Awake()
    {
        type = Type.Attack;
        name = "Spell_Mushroom";

    }


    public override void InitSpell()
    {
        base.InitSpell();
        pet.SetActive(true);
        pet.transform.position = Player.instance.transform.Find("Head").transform.position;
    }


    public override void Clean()
    {
        isActivate = false;
        pet.SetActive(false);
    }
    private void Update()
    {

        if (Input.GetButtonDown(Dico.Get("BUTTON_SPELL")))
        {
            Bomb();
        }
    }


    public BombBehaviour BombPrefab;
    public void Bomb()
    {
        if (timeBtwSpell < 0 && isActivate)
        {
            Vector3 BombPosition = Player.instance.transform.position;
            BombPosition.x = BombPosition.x - 1;
            BombBehaviour bb = Instantiate(BombPrefab, BombPosition, new Quaternion());
            timeBtwSpell = startTimeBtwSpell;
        }
    }
}
