using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private ControllerHealth mobControllerHealth;

    void Awake()
    {
        mobControllerHealth = transform.GetComponentInParent<ControllerHealth>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(gameObject.layer) == "Secret")
        {
            GetComponent<FadeOut>().canFade = true;
        }
        else
            mobControllerHealth.TriggerMob(collision);
    }
}
