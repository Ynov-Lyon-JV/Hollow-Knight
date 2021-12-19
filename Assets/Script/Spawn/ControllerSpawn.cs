using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSpawn : MonoBehaviour
{
    public static ControllerSpawn instance;

    [NonSerialized]
    public string pos = "";

    private GameObject[] respawns;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ControllerSpawn dans la scène");
            return;
        }

        instance = this;
    }

    public void Spawn()
    {
        if (pos.Length > 1)
        {
            pos = Dico.Get(pos);
            GameObject spawn = GameObject.Find(pos);
            if (spawn)
                transform.position = spawn.transform.position;
            else
                RespawnNear();
        }
        else
        {
            RespawnNear();
        }
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
            Respawn(respawns[posNear].transform.position);
    }
    public void Respawn(Vector3 posVector)
    {
        StartCoroutine(CaroutineRespawn(posVector));
    }
    public IEnumerator CaroutineRespawn(Vector3 posVector)
    {
        PlayerMove.instance.enabled = false;
        GameObject.Find("UI").GetComponentInChildren<ControllerAnimation>().ChangeAnimationState(Dico.Get("ANIM_TRANSITION_FADEIN"));
        yield return new WaitForSeconds(0.5f);
        transform.position = posVector;
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("UI").GetComponentInChildren<ControllerAnimation>().ChangeAnimationState(Dico.Get("ANIM_TRANSITION_FADEOUT"));
        yield return new WaitForSeconds(0.3f);
        PlayerMove.instance.enabled = true;

    }
}
