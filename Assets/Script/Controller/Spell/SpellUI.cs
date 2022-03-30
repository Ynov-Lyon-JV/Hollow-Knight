using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Affiche()
    {
        foreach (Spell spell in ControllerSpell.instance.spells)
        {
            if (!spell.isUnlock)
                continue;
            Instantiate(Resources.Load<GameObject>("Player/Spells/Spell"), transform.position, new Quaternion(),GameObject.Find("UI_Spells").transform);

        }
    }
}
