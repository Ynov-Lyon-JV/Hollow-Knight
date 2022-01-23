using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBorder : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = Player.instance;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision is CapsuleCollider2D)
        {
            Player.instance.controllerHealth.TakeDamage(1, false, 0, false);
            if (Player.instance.controllerHealth.Health <= 0)
            {
                return;
            }

    collision.gameObject.GetComponent<ControllerSpawn>().RespawnNear();

        }
    }
}
