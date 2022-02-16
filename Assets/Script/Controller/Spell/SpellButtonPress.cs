using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class SpellButtonPress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {

        ControllerSpell.instance.GetSpell(transform.parent.name);
    }


    public void Stop()
    {

        ControllerSpell.instance.StopSpell(transform.parent.name);
    }
}
