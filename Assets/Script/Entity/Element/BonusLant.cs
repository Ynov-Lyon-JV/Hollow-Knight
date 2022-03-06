using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLant : Lanterne
{
    public override void Create()
    {
        GameObject pv = Instantiate(Resources.Load<GameObject>("Level/Follet/Follet_Bonus"), new Vector2(GetComponent<Collider2D>().bounds.min.x - 1, GetComponent<Collider2D>().bounds.max.y), Quaternion.identity);
        pv.name = "bonus";
    }
}
