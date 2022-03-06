using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    private bool isTrigger;
    public virtual bool IsTrigger
    {
        get { return isTrigger; }
        set
        {
            isTrigger = value;
            transform.Find("Interact").gameObject.SetActive(value);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTrigger && Input.GetButtonDown(Dico.Get("BUTTON_INTERACT")))
        {
            Use();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            IsTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsTrigger = false;
            transform.Find("Text").gameObject.SetActive(false);
        }
    }
    virtual protected void Use()
    {
        bool active = true;
        if (transform.Find("Text").gameObject.activeSelf)
        {
            active = false;
        }
        transform.Find("Text").gameObject.SetActive(active);
        transform.Find("Interact").gameObject.SetActive(!active);
    }
}
