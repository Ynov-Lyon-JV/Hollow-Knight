using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMushroom : Spell
{
    private new Renderer renderer;
    private GameObject pet;
    private void Awake()
    {
        type = Type.Summuner;
        name = "Spell_Bat";

        foreach (GameObject gObject in GameObject.FindGameObjectsWithTag("Pet"))
        {
            if (gObject.name == "PetBat")
            {
                pet = gObject;
                Clean();
                return;
            }
        }
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
}
