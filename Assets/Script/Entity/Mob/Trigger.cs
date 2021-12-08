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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        mobControllerHealth.TriggerMob(collision);
    }
}
