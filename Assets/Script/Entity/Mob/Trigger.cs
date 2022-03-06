using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private string type;
    private bool isFirstTime = true;

    private void Awake()
    {
        type = LayerMask.LayerToName(gameObject.layer);
        if(type=="Default")
            type = LayerMask.LayerToName(transform.parent.gameObject.layer);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (type)
        {
            case "Mob":
            case "Trap":
                GetComponentInParent<ControllerHealth>().TriggerMob(collision, "Player");
                break;
            case "Pet":
                GetComponentInParent<ControllerHealth>().TriggerMob(collision, "Mob");
                break;
            case "Secret":
                if (collision.gameObject.CompareTag("Player"))
                    GetComponent<FadeOut>().canFade = true;
                break;
            case "Attract Item":
                if (collision.gameObject.CompareTag("Player"))
                {
                    GetComponent<FadeOut>().canFade = true;
                    if (isFirstTime)
                    {
                        if (transform.name == "pv")
                        {
                            Player.instance.controllerHealth.Health++;
                        }
                        else if (transform.name == "bonus")
                        {
                            Player.instance.Bonus++;
                        }
                        else
                        {
                        ControllerSpell.instance.XpSpell(transform.name);
                        }
                        isFirstTime = false;
                    }
                }
                break;
        }
    }
}
