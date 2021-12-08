using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBorder : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ControllerHealth>().TakeDamage(1,false);
            collision.gameObject.GetComponent<ControllerSpawn>().RespawnNear();
        }
    }
}
