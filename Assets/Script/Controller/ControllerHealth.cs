using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHealth : MonoBehaviour
{

    [SerializeField]
    private int health;

    [SerializeField]
    [Tooltip("De base: 0.6")]
    private float startDazedTime;
    private float dazedTime;

    private ControllerMove controllerMove;

    [SerializeField]
    private GameObject bloodEffect;

    // Start is called before the first frame update
    void Awake()
    {
        controllerMove = this.transform.GetComponent<ControllerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dazedTime <= 0)
        {
            controllerMove.speed = controllerMove.speedBase;
        }
        else
        {
            controllerMove.speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("Damage Taken");
    }
}
