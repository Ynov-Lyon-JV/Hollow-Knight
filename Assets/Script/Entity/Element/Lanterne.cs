using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lanterne : Entity
{
    private new string name = "ERROR";
    private void Awake()
    {
        name = SceneManager.GetActiveScene().name + "_" + transform.name;
        if (name!=null && ListeLant.instance.Verif(name))
            Delete();
    }
    public override void Destroy()
    {
        Delete();
        Create();
        ListeLant.instance.liste.Add(name);
    }
    private void Delete()
    {
        Destroy(GetComponent<ControllerHealth>());
        GetComponent<ChangeSprite>().NewSprite();
    }

    virtual public void Create()
    {
        GameObject pv = Instantiate(Resources.Load<GameObject>("Level/Follet/Follet"), new Vector2(GetComponent<Collider2D>().bounds.min.x - 1, GetComponent<Collider2D>().bounds.max.y), Quaternion.identity);
        pv.name = "pv";
    }
}
