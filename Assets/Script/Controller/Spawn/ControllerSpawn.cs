using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSpawn : MonoBehaviour
{
    public Transform pos;

    public GameObject[] respawns;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        transform.position = pos.position;
    }

    public void RespawnNear() //Le spwan le plus proche débloqué
    {
        respawns = GameObject.FindGameObjectsWithTag("Respawn");
        if (respawns.Length == 0)
            return;
        int posNear = -1;
        for (int i = 0; i < respawns.Length; i++)
        {
            if (!respawns[i].GetComponent<SpawnPoint>().spawnPointLock)
            {
                if (posNear == -1 || Vector2.Distance(respawns[i].transform.position, transform.position) < Vector2.Distance(respawns[posNear].transform.position, transform.position))
                    posNear = i;
            }
        }
        if (posNear != -1)
            transform.position = respawns[posNear].transform.position;
    }
}
