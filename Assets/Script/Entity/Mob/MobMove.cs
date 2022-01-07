
using System;
using System.Collections;
using UnityEngine;

public abstract class MobMove : MonoBehaviour
{


    #region Move
    [NonSerialized]
    public ControllerMove CM;

    private int timeDirectionLock = 0;


    public virtual void Move()
    {
        MoveBasic();
    }

    protected void MoveBasic()
    {
        if (!CM.IsGrounded() && timeDirectionLock <= 0)
        {
            Flip();
            timeDirectionLock = 3; //Permet d'éviter les bugs
        }
        else if (timeDirectionLock > 0)
        {
            timeDirectionLock--;
        }
        CM.Move(CM.direction / CM.slowTempo);
    }

    public void Flip()
    {
        CM.direction = -CM.direction;
        CM.slowTempo = 100;
    }
    #endregion

    #region Detect
    [SerializeField]
    [Tooltip("De base: 2")]
    private float startTimeDetect = 2;
    private float timeDetect = 0;

    private bool detect;
    public bool Detect
    {
        get { return detect; }
        set
        {
            detect = value;
            transform.Find("Detect").gameObject.SetActive(value);

            if (value)
            {
                timeDetect = startTimeDetect;
            }
        }
    }

    void Update()
    {
        if (timeDetect > 0)
        {
            timeDetect -= Time.deltaTime;
        }
        else if (Detect)
        {
            Detect = false;
        }
    }
    #endregion


    #region Destroy

    public virtual void Destroy()
    {
        Destroy(GetComponent<ControllerMove>());
        Destroy(GetComponent<ControllerHealth>());
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GetComponent<FadeOut>().canFade = true;
    }

    #endregion
}



