using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesCollision : MonoBehaviour
{

    private MobMove mobMove;

    // Start is called before the first frame update
    void Awake()
    {
        mobMove = transform.GetComponentInParent<MobMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapPoint(transform.position, mobMove.CM.whatisGround))
            mobMove.Flip();
    }
}
