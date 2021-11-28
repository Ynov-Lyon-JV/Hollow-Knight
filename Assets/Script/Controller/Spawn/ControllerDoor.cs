using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerDoor : MonoBehaviour
{
    public string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(loadNextScene());
        }
    }


    public IEnumerator loadNextScene()
    {
        PlayerMove.instance.enabled = false;
        GameObject.Find("UI").GetComponentInChildren<ControllerAnimation>().ChangeAnimationState(Dico.Get("ANIM_TRANSITION_FADEIN"));
        //LoadAndSaveData.instance.SaveData();
        yield return new WaitForSeconds(0.5f);
        ControllerSpawn.instance.pos = this.transform.GetChild(0).name;


        SceneManager.LoadScene(scene);
    }
}