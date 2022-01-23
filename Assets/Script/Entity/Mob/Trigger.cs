using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private string type;
    private void Awake()
    {
        type = LayerMask.LayerToName(transform.parent.gameObject.layer);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (type == "Mob" || type == "Trap")
        {
            GetComponentInParent<ControllerHealth>().TriggerMob(collision,"Player");
        }
        else if (type == "Pet")
        {
            GetComponentInParent<ControllerHealth>().TriggerMob(collision,"Mob");
        }
        else if (type == "Secret")
        {
            if(collision.gameObject.CompareTag("Player"))
                GetComponent<FadeOut>().canFade = true;
        }
    }
}
