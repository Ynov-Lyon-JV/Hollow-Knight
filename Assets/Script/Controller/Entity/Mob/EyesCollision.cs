using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesCollision : MonoBehaviour
{

    private Mob mobMove;

    // Start is called before the first frame update
    void Awake()
    {
        mobMove = transform.GetComponentInParent<Mob>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics2D.OverlapPoint(transform.position, mobMove.controllerMove.whatisGround))
        {
            if(mobMove.Detect)
                mobMove.Detect = false;
            mobMove.Flip();
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mobMove.Detect = true;
        }
    }

}
