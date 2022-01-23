using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{

    protected Mob mob;

    [SerializeField]
    private bool whoDetect = false;

    // Start is called before the first frame update
    void Awake()
    {
        mob = transform.GetComponentInParent<Mob>();
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (whoDetect)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == "Mob")
            {
                mob.Detect = true;
                mob.whoDetect = collision.gameObject.transform;
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            mob.Detect = true;
            mob.whoDetect = collision.gameObject.transform;
        }



    }

}
