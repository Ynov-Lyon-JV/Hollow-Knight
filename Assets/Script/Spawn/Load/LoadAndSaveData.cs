using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;


    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    
    void Start()
    {
        ControllerSpawn.instance.Spawn();
        GameObject g = GameObject.Find("Cameras");
        CinemachineConfiner2D cinemachineConfiner = g.GetComponentInChildren<CinemachineConfiner2D>();
        cinemachineConfiner.m_BoundingShape2D = GameObject.Find("Background").GetComponent<PolygonCollider2D>();

        StartCoroutine(LoadFadeOut());
    }
    /*
        public void SaveData()
        {
            PlayerPrefs.SetInt("coinsCount", Inventory.instance.coinsCount);

            if (CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
            {
                PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
            }

            string itemsInInventory = string.Join(",", Inventory.instance.content.Select(x => x.id));
            PlayerPrefs.SetString("inventoryItems", itemsInInventory);
        }*/


    public IEnumerator LoadFadeOut()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("UI_Transition").GetComponent<ControllerAnimation>().ChangeAnimationState(Dico.Get("ANIM_TRANSITION_FADEOUT"));
        yield return new WaitForSeconds(0.1f);
        Player.instance.Resume();
    }
}